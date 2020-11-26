using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class DefineRoleForm : Form
    {
        MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        UpdateOnCloseDel UpdateOnClose = null;
        public DefineRoleForm(UpdateOnCloseDel UpdateOnClose)
        {
            InitializeComponent();
            cmbSelectRole.Items.Clear();
            FillRoles();
            cmbSelectRole.SelectedIndex = 0;
            cmbSelectRole.Focus();
            this.UpdateOnClose = UpdateOnClose;
            this.FormClosed += DefineRoleForm_FormClosed;
            DynamicAddCheckBox();
        }

        private void DynamicAddCheckBox()
        {
            try
            {
                List<string> ListPrivilege = CommonFunctions.ObjUserMasterModel.GetAllPrivilegeNames();

                for (int i = 0; i < ListPrivilege.Count; i++)
                {
                    CheckBox chk = new CheckBox();
                    //chk.Width = 80;
                    chk.Text = ListPrivilege[i];
                    chk.Name = "chbx" + ListPrivilege[i];
                    chk.Checked = false;
                    chk.CheckedChanged += new EventHandler(chk_ChangedCheck);
                    flpChsePrivilege.Controls.Add(chk);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DefineRoleForm.DynamicAddCheckBox()", ex);
                throw;
            }
        }
        private void ResetPrivilegeChkBox()
        {
            try
            {
                for (int i = 0; i < flpChsePrivilege.Controls.Count; i++)
                {
                    if (flpChsePrivilege.Controls[i] is CheckBox)
                    {
                        ((CheckBox)(flpChsePrivilege.Controls[i])).Checked = false;
                        //if (chk.Checked == true) chk.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DefineRoleForm.ResetPrivilegeChkBox()", ex);
                throw;
            }
        }
        private void FillRoles()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListRoles = CommonFunctions.ObjUserMasterModel.GetAllRoles();
                cmbSelectRole.Items.Add("Select Role");
                foreach (var item in ListRoles)
                {
                    //cmbxSelectRoleID.Items.Add(item.Cast<object>());
                    cmbSelectRole.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DefineRoleForm.FillRoles()", ex);
                throw;
            }
        }
        private void chk_ChangedCheck(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DefineRoleForm.chk_ChangedCheck()", ex);
                throw ex;
            }

        }

        private void btnDefineRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSelectRole.SelectedIndex == 0)
                {
                    lblSelectRoleValidMsg.Visible = true;
                    lblSelectRoleValidMsg.Text = "Please select a role";
                    cmbSelectRole.Focus();
                    return;
                }
                else lblSelectRoleValidMsg.Visible = false;

                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNames = new List<string>();
                // List<string> ListTemp = tmpMySQLHelper.GetAllPrivileges();
                MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListTemp = new List<string>();
                for (int i = 0; i < flpChsePrivilege.Controls.Count; i++)
                {
                    if (flpChsePrivilege.Controls[i] is CheckBox)
                    {
                        CheckBox chk = (CheckBox)(flpChsePrivilege.Controls[i]);
                        string boolVal = "YES";
                        if (chk.Checked == true)
                        {
                            boolVal = ",YES";
                        }
                        else
                        {
                            boolVal = ",NO";
                        }
                        ListTemp.Add(CommonFunctions.ObjUserMasterModel.GetPrivilegeID(chk.Text) + boolVal);
                    }
                }
                string[] arr = null;
                for (int i = 0; i < ListTemp.Count; i++)
                {
                    arr = ListTemp[i].Split(',');
                    ListColumnValues.Add(arr[1]);
                    ListColumnNames.Add(arr[0]);
                    //ListColumnNamesWithDataType.Add(ListTemp[i] + ",TINYTEXT");
                }
                string WhereCondition = "ROLENAME = '" + cmbSelectRole.SelectedItem + "'";
                int ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("ROLEMASTER", ListColumnNames, ListColumnValues, WhereCondition);
                if (ResultVal < 0) MessageBox.Show("Wasnt able to create  role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2) MessageBox.Show("Role already Exists, Please try adding new Role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show("Updated Role :: " + cmbSelectRole.SelectedItem + " successfully", "Role Update");
                    UpdateOnClose(2);
                    btnReset.PerformClick();

                    // if (ObjManageUsers != null) ObjManageUsers.BindGrid();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DefineRoleForm.btnDefineRole_Click()", ex);
                throw;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                cmbSelectRole.SelectedIndex = 0;
                ResetPrivilegeChkBox();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DefineRoleForm.btnReset_Click()", ex);
                throw;
            }
        }

        private void DefineRoleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // UpdateOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DefineRoleForm.DefineRoleForm_FormClosed()", ex);
            }
        }



        private void DefineRoleForm_Load(object sender, EventArgs e)
        {

        }

        private void cmbSelectRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectedIndex != 0)
                {
                    string RoleName = (string)comboBox.SelectedItem;
                    List<bool> ListPrivilegeAssigned = CommonFunctions.ObjUserMasterModel.GetAllPrivilegeValuesAssignedForARole(RoleName);
                    List<string> ListPrivilege = CommonFunctions.ObjUserMasterModel.GetAllPrivilegeNames();
                    //RoleDetails ObjRoleDetails = CommonFunctions.ObjUserMasterModel.(RoleName);
                    for (int i = 0; i < ListPrivilege.Count; i++)
                    {
                        foreach (Control ObjControl in flpChsePrivilege.Controls)
                        {
                            if (ObjControl is CheckBox)
                            {
                                if (((CheckBox)ObjControl).Name == "chbx" + ListPrivilege[i])
                                {
                                    ((CheckBox)ObjControl).Checked = ListPrivilegeAssigned[i];
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DefineRoleForm.cmbSelectRole_SelectedIndexChanged()", ex);
                throw;
            }
        }


        //private void btnAddPrivilege_Click(object sender, EventArgs e)
        //{
        //    if (txtPrivilegeName.Text.Trim() == string.Empty)
        //    {
        //        lblAddPrivilegeValidmsg.Visible = true;
        //        lblAddPrivilegeValidmsg.Text = "Privilege Name cant be empty !";
        //        return;
        //    }
        //    else
        //    {
        //        lblAddPrivilegeValidmsg.Visible = false;

        //        int ResultVal = CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_16", txtPrivilegeName.Text);
        //        //int ResultVal = 0;
        //        if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the Previlege", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        else if (ResultVal == 2)
        //        {
        //            MessageBox.Show("Privilege Name already exists! Pls Provide Another Name.", "Error");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Added New Privilege :: " + txtPrivilegeName.Text + " successfully", "Added Privilege");
        //            tmpMySQLHelper.AlterTblAddColumn("ROLEMASTER", "P_16", "TINYTEXT");
        //            //CommonFunctions.ObjUserMasterModel.AlterTblColBasedOnMultipleRowsFrmAnotherTbl("PRIVILEGEMASTER", "ROLEMASTER", "PRIVILEGEID");
        //            UpdateOnClose(1);
        //            DynamicAddCheckBox();
        //            btnReset.PerformClick();
        //            //if (ObjManageUsers != null) ObjManageUsers.BindGrid();//&&&&&
        //        }

        //    }
        //}
    }
}
