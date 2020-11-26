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
    public partial class CreateRoleForm : Form
    {
        UpdateOnCloseDel UpdateOnClose = null;
        public CreateRoleForm(UpdateOnCloseDel UpdateOnClose)
        {
            InitializeComponent();
            txtNewRoleName.Focus();
            this.UpdateOnClose = UpdateOnClose;
            this.FormClosed += CreateRoleForm_FormClosed;
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
                    chk.CheckedChanged += new EventHandler(chk_ChangedCheck);
                    flpChsePrivilege.Controls.Add(chk);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateRole.DynamicAddCheckBox()", ex);
                throw ex;
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
                CommonFunctions.ShowErrorDialog("CreateRole.chk_ChangedCheck()", ex);
                throw ex;
            }
        }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewRoleName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter role name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNewRoleName.Focus();
                    return;
                }
                if (txtRoleDesc.Text.Trim() == "")
                {
                    MessageBox.Show("Please add description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRoleDesc.Focus();
                    return;
                }

                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNamesWithDataType = new List<string>();
                MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListTemp = new List<string>();
                for (int i = 0; i < flpChsePrivilege.Controls.Count; i++)
                {
                    if (flpChsePrivilege.Controls[i] is CheckBox)
                    {
                        CheckBox chk = (CheckBox)(flpChsePrivilege.Controls[i]);
                        if (chk.Checked == true)
                        {
                            ListTemp.Add(CommonFunctions.ObjUserMasterModel.GetPrivilegeID(chk.Text));
                        }
                    }
                }

                for (int i = 0; i < ListTemp.Count; i++)
                {
                    ListColumnValues.Add("YES");
                    ListColumnNamesWithDataType.Add(ListTemp[i] + ",TINYTEXT");
                }

                int ResultVal = CommonFunctions.ObjUserMasterModel.CreateNewRole(txtNewRoleName.Text, txtRoleDesc.Text, ListColumnNamesWithDataType, ListColumnValues);
                if (ResultVal < 0) MessageBox.Show("Wasnt able to create  role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2) MessageBox.Show("Role already Exists, Please try adding new Role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show("New Role :: " + txtNewRoleName.Text + " added successfully", "Role Added");
                    UpdateOnClose(Mode: 2);
                    btnReset.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateRole.btnCreateRole_Click()", ex);
                throw ex;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtNewRoleName.Clear();
                txtRoleDesc.Clear();
                txtNewRoleName.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateRole.btnReset_Click()", ex);
                throw ex;
            }
        }

        private void CreateRoleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //UpdateOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateRoleForm.CreateRoleForm_FormClosed()", ex);
            }
        }
    }
}
