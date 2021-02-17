using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public delegate void UpdateOnCloseDel(Int32 Mode);
    public partial class ManageUsersForm : Form
    {
        MySQLHelper tmpMySQlHelper = MySQLHelper.GetMySqlHelperObj();
        CheckBox headerCheckBox = new CheckBox();
        public ManageUsersForm()
        {
            InitializeComponent();
        }
        private void btnRedirectCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                //CreateUserForm ObjCreateUserForm = new CreateUserForm(this);
                //ObjCreateUserForm.Show();
                //CommonFunctions.ShowDialog(ObjCreateUserForm,this);
                CommonFunctions.ShowDialog(new CreateUserForm(UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.btnRedirectCreateUser_Click()", ex);
                throw ex;
            }
        }

        private void btnRedirectEditUser_Click(object sender, EventArgs e)
        {
            try
            {
                EditUserForm ObjEditUserForm = new EditUserForm(UpdateOnClose);

                DataGridViewRow CurrentRow = GetSelectedRowFrmGrid();
                if (CurrentRow == null) return;

                ObjEditUserForm.txtUserName.Text = CurrentRow.Cells["USERNAME"].Value.ToString();
                ObjEditUserForm.txtFullName.Text = CurrentRow.Cells["FULLNAME"].Value.ToString();
                ObjEditUserForm.cmbxSelectRoleID.SelectedItem = CurrentRow.Cells["ROLENAME"].Value.ToString();
                ObjEditUserForm.txtEmailID.Text = CurrentRow.Cells["EMAILID"].Value.ToString();
                ObjEditUserForm.cmbxSelectStore.SelectedItem = CurrentRow.Cells["STORENAME"].Value.ToString();
                if (CurrentRow.Cells["USERNAME"].Value.ToString().ToUpper() != "ADMIN")
                {
                    if (Convert.ToBoolean(CurrentRow.Cells["ACTIVE"].Value) == true)
                        ObjEditUserForm.rdbtnActiveYes.Checked = true;
                    else ObjEditUserForm.rdbtnActiveNo.Checked = true;
                }
                else
                {
                    ObjEditUserForm.rdbtnActiveYes.Checked = true;
                    ObjEditUserForm.rdbtnActiveNo.Enabled = false;
                }
                ObjEditUserForm.txtPhone.Text = CurrentRow.Cells["PHONENO"].Value.ToString();
                if (ObjEditUserForm.txtPhone.Text == "0") ObjEditUserForm.txtPhone.Text = "";

                CommonFunctions.ShowDialog(ObjEditUserForm, this);


                #region CheckOnce
                //if (headerCheckBox.Checked == false)
                //{
                //    if (dgvUserCache.CurrentRow.Cells["checkBoxColumn"].Value != null && dgvUserCache.CurrentRow.Cells["checkBoxColumn"].Value.ToString().ToUpper() == "TRUE")
                //    {


                //        // ObjEditUserForm.txtUserName.Text = dgvUserCache.CurrentRow.Cells[1].Value.ToString();
                //        //ObjEditUserForm.txtUserName.Text = dgvUserCache.CurrentRow.Cells[dgvUserCache.Columns["USERNAME"].Index].Value.ToString();
                //        //ObjEditUserForm.txtFullName.Text = dgvUserCache.CurrentRow.Cells[dgvUserCache.Columns["FULLNAME"].Index].Value.ToString();
                //        //ObjEditUserForm.cmbxSelectRoleID.SelectedItem = dgvUserCache.CurrentRow.Cells[dgvUserCache.Columns["ROLENAME"].Index].Value.ToString();
                //        //ObjEditUserForm.txtEmailID.Text = dgvUserCache.CurrentRow.Cells[dgvUserCache.Columns["EMAILID"].Index].Value.ToString();

                //        //if (Convert.ToBoolean(dgvUserCache.CurrentRow.Cells[dgvUserCache.Columns["ACTIVE"].Index].Value) == true)
                //        //    ObjEditUserForm.rdbtnActiveYes.Checked = true;
                //        //else ObjEditUserForm.rdbtnActiveNo.Checked = true;
                //        //ObjEditUserForm.txtPhone.Text = dgvUserCache.CurrentRow.Cells[dgvUserCache.Columns["PHONENO"].Index].Value.ToString();
                //        ObjEditUserForm.txtUserName.Text = dgvUserCache.CurrentRow.Cells["USERNAME"].Value.ToString();
                //        ObjEditUserForm.txtFullName.Text = dgvUserCache.CurrentRow.Cells["FULLNAME"].Value.ToString();
                //        ObjEditUserForm.cmbxSelectRoleID.SelectedItem = dgvUserCache.CurrentRow.Cells["ROLENAME"].Value.ToString();
                //        ObjEditUserForm.txtEmailID.Text = dgvUserCache.CurrentRow.Cells["EMAILID"].Value.ToString();
                //        ObjEditUserForm.cmbxSelectStore.SelectedItem = dgvUserCache.CurrentRow.Cells["STORENAME"].Value.ToString();
                //        if (Convert.ToBoolean(dgvUserCache.CurrentRow.Cells["ACTIVE"].Value) == true)
                //            ObjEditUserForm.rdbtnActiveYes.Checked = true;
                //        else ObjEditUserForm.rdbtnActiveNo.Checked = true;
                //        ObjEditUserForm.txtPhone.Text = dgvUserCache.CurrentRow.Cells["PHONENO"].Value.ToString();
                //        if (ObjEditUserForm.txtPhone.Text == "0") ObjEditUserForm.txtPhone.Text = "";


                //        //ObjEditUserForm.Show();
                //        CommonFunctions.ShowDialog(ObjEditUserForm, this);

                //    }
                //    else
                //    {
                //        MessageBox.Show("Please Select an User To Edit!", "Error");
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Please Select any one User To Edit!", "Error");
                //}
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.btnRedirectEditUser_Click()", ex);
                throw;
            }

        }



        private void btnReloadUserCache_Click(object sender, EventArgs e)
        {
            try
            {
                this.BindUserGrid(true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.btnReloadUserCache_Click()", ex);
                throw ex;
            }
        }

        public void EnableOrDisableBtnBasedOnUserPrivilege()
        {
            try
            {
                foreach (Control ControlItem in panelAllBtnTab.Controls)
                {
                    Button btnControl = (Button)(ControlItem);
                    btnControl.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.EnableOrDisableBtnBasedOnUserPrivilege()", ex);
                throw ex;
            }
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            try
            {
                //EnableOrDisableBtnBasedOnUserPrivilege();


                this.BindUserGrid(false); //&&&&& should be true?
                dgvUserCache.AutoResizeColumns();
                dgvUserCache.AutoResizeRows();
                dgvUserCache.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.ManageUsers_Load()", ex);
                throw;
            }
        }
        public DataGridViewRow GetSelectedRowFrmGrid()
        {
            try
            {
                DataGridViewRow SelectedRow = null;
                int CountOfSelectedRow = 0;
                foreach (DataGridViewRow ItemRow in dgvUserCache.Rows)
                {
                    //CheckBox chk = (CheckBox)ItemRow.Cells["checkBoxColumn"];
                    if (ItemRow.Cells["checkBoxColumn"].Value != null && ItemRow.Cells["checkBoxColumn"].Value.ToString().ToUpper() == "TRUE")
                    {
                        
                        CountOfSelectedRow++;
                        if (CountOfSelectedRow > 1)
                        {
                            MessageBox.Show("Please Select any one User To Edit!", "Error");
                            return null;
                        }
                        SelectedRow = ItemRow;
                    }
                }
                if (CountOfSelectedRow == 0)
                {
                    MessageBox.Show("Please Select an User To Edit!", "Error");
                    return null;
                }
                return SelectedRow;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.GetSelectedRowFrmGrid()", ex);
                throw;
            }


        }
        public void BindUserGrid(Boolean ReloadFromDB)
        {
            try
            {
                dgvUserCache.DataSource = null;
                dgvUserCache.Refresh();
                //dgvUserCache.DataSource = CommonFunctions.ObjUserMasterModel.FillMangeUserCacheGrid();
                LoadUserDetailsDataGridView(ReloadFromDB);
                // if (!dgvUserCache.Controls.Contains(headerCheckBox))
                // {
                AddCheckBoxToDGV();
                //}
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.BindUserGrid()", ex);
                throw;
            }
        }
        void LoadUserDetailsDataGridView(Boolean ReloadFromDB)
        {
            try
            {
                if (ReloadFromDB) CommonFunctions.ObjUserMasterModel.LoadAllUserMasterTables();

                dgvUserCache.Rows.Clear();
                dgvUserCache.Columns.Clear();
                String[] ArrColumnNames = new String[] { "USERID", "ROLEID", "STOREID", "USERNAME", "FULLNAME", "ROLENAME", "STORENAME", "EMAILID", "PHONENO", "CREATEDBY", "ACTIVE", "LASTLOGIN", "LASTPASSWORDCHANGED", "LASTUPDATEDATE" };
                //USERID,USERNAME,FULLNAME,EMAILID,ROLEID,LASTLOGIN,LASTPASSWORDCHANGED,LASTUPDATEDATE,IF(ACTIVE='1','true','false')ACTIVE,PHONENO,CREATEDBY
                String[] ArrColumnHeaders = new String[] { "UserID", "RoleID", "StoreID", "User Name", "Full Name", "Role", "Store", "EmailID", "Phone No", "Created By", "Active", "Last Login", "Last Password Changed", "Last Update Date" };
                for (int i = 0; i < ArrColumnNames.Length; i++)
                {
                    dgvUserCache.Columns.Add(ArrColumnNames[i], ArrColumnHeaders[i]);
                    DataGridViewColumn CurrentCol = dgvUserCache.Columns[dgvUserCache.Columns.Count - 1];
                    CurrentCol.ReadOnly = true;
                    if (i <= 2) CurrentCol.Visible = false;    //UserId,RoleId,StoreId,
                }

                foreach (UserDetails ObjUserDetails in CommonFunctions.ObjUserMasterModel.GetListUserCache())
                {
                    Object[] ArrRowItems = new Object[14];
                    ArrRowItems[0] = ObjUserDetails.UserID;
                    ArrRowItems[1] = ObjUserDetails.RoleID;
                    ArrRowItems[2] = ObjUserDetails.StoreID;
                    ArrRowItems[3] = ObjUserDetails.UserName;
                    ArrRowItems[4] = ObjUserDetails.FullName;
                    ArrRowItems[5] = ObjUserDetails.RoleName;
                    ArrRowItems[6] = ObjUserDetails.StoreName;
                    ArrRowItems[7] = ObjUserDetails.EmailID;
                    ArrRowItems[8] = ObjUserDetails.PhoneNo == 0 ? "" : ObjUserDetails.PhoneNo.ToString();
                    ArrRowItems[9] = CommonFunctions.ObjUserMasterModel.GetUserName(ObjUserDetails.CreatedBy);
                    ArrRowItems[10] = ObjUserDetails.Active;
                    ArrRowItems[11] = ObjUserDetails.LastLogin == DateTime.MinValue ? DateTime.Now : ObjUserDetails.LastLogin;
                    ArrRowItems[12] = ObjUserDetails.LastPasswordChanged == DateTime.MinValue ? DateTime.Now : ObjUserDetails.LastPasswordChanged;
                    ArrRowItems[13] = ObjUserDetails.LastUpdateDate == DateTime.MinValue ? DateTime.Now : ObjUserDetails.LastUpdateDate;

                    dgvUserCache.Rows.Add(ArrRowItems);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.LoadUserDetailsDataGridView()", ex);
                throw;
            }
        }

        private void AddCheckBoxToDGV()
        {
            try
            {
                //Add a CheckBox Column to the DataGridView Header Cell.

                //Find the Location of Header Cell.
                Point headerCellLocation = this.dgvUserCache.GetCellDisplayRectangle(3, -1, true).Location;

                //Place the Header CheckBox in the Location of the Header Cell.
                //headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
                headerCheckBox.Location = new Point(headerCellLocation.X + 2, headerCellLocation.Y + 3);
                headerCheckBox.BackColor = Color.White;
                headerCheckBox.Size = new Size(18, 18);
                if (headerCheckBox.Checked == true) headerCheckBox.Checked = false;
                //Assign Click event to the Header CheckBox.
                headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
                dgvUserCache.Controls.Add(headerCheckBox);

                //Add a CheckBox Column to the DataGridView at the first position.
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "";
                checkBoxColumn.Width = 30;
                checkBoxColumn.Name = "checkBoxColumn";
                dgvUserCache.Columns.Insert(3, checkBoxColumn);

                //Assign Click event to the DataGridView Cell.
                dgvUserCache.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClick);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.AddCheckBoxToDGV()", ex);
                throw;
            }

        }
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Necessary to end the edit mode of the Cell.
                dgvUserCache.EndEdit();

                //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
                foreach (DataGridViewRow row in dgvUserCache.Rows)
                {
                    DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                    checkBox.Value = headerCheckBox.Checked;
                    if (Convert.ToBoolean(checkBox.Value) == true)
                        row.DefaultCellStyle.BackColor = Color.RoyalBlue;//System.Drawing.ColorTranslator.FromHtml("#0078d7");#;
                    else row.DefaultCellStyle.BackColor = Color.White;
                    //row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.HeaderCheckBox_Clicked()", ex);
                throw;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Check to ensure that the row CheckBox is clicked.
                if (e.RowIndex >= 0 && e.ColumnIndex == dgvUserCache.Columns["checkBoxColumn"].Index)
                {
                    //Loop to verify whether all row CheckBoxes are checked or not.
                    bool isChecked = true;
                    foreach (DataGridViewRow row in dgvUserCache.Rows)
                    {
                        DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                        //if (Convert.ToBoolean(row.Cells["checkBoxColumn"].EditedFormattedValue) == false)
                        if (Convert.ToBoolean(checkBox.EditedFormattedValue) == false)
                        {
                            //row.Selected = true;
                            if (isChecked) isChecked = false;
                            //break;
                            row.DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.RoyalBlue;
                        }
                        //if (Convert.ToBoolean(checkBox.Value) == true)
                        //    row.DefaultCellStyle.BackColor = Color.RoyalBlue;//System.Drawing.ColorTranslator.FromHtml("#0078d7");#;
                        //else row.DefaultCellStyle.BackColor = Color.White;
                    }
                    headerCheckBox.Checked = isChecked;

                }
                //dgvUserCache.EndEdit();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.DataGridView_CellClick()", ex);
                throw;
            }
        }

        private void btnRedirectDeleteUser_Click(object sender, EventArgs e)
        {

            try
            {
                List<int> ListUserIDs = new List<int>();
                #region CheckOnce
                //if (headerCheckBox.Checked == false)
                //{

                //    if (dgvUserCache.CurrentRow.Cells[dgvUserCache.Columns["checkBoxColumn"].Index].Value != null && dgvUserCache.CurrentRow.Cells["checkBoxColumn"].Value.ToString().ToUpper() == "TRUE")
                //    {
                //        if (dgvUserCache.CurrentRow.Cells["ACTIVE"].Value.ToString().ToUpper() == "FALSE")
                //        {
                //            MessageBox.Show("The User is already InActive, To change the state pls use 'Edit User' section", "Error", MessageBoxButtons.OK);
                //            return;
                //        }
                //        //ObjEditUserForm.txbxUserID.Text = dgvUserCache.CurrentRow.Cells[1].Value.ToString();
                //        //ObjEditUserForm.txtEditUserName.Text = dgvUserCache.CurrentRow.Cells[2].Value.ToString();
                //        //ObjEditUserForm.txtFullName.Text = dgvUserCache.CurrentRow.Cells[3].Value.ToString();
                //        //ObjEditUserForm.txtEmailID.Text = dgvUserCache.CurrentRow.Cells[4].Value.ToString();
                //        //ObjEditUserForm.cmbxSelectRoleID.Text = dgvUserCache.CurrentRow.Cells[5].Value.ToString();
                //        //if (dgvUserCache.CurrentRow.Cells[9].Value.ToString().ToUpper() == "YES")
                //        //    ObjEditUserForm.rdbtnActiveYes.Checked = true;
                //        //else ObjEditUserForm.rdbtnActiveNo.Checked = true;
                //        //ObjEditUserForm.txtPhone.Text = dgvUserCache.CurrentRow.Cells[10].Value.ToString();
                //        //ObjEditUserForm.Show();
                //        //USERID,USERNAME,FULLNAME,EMAILID,ROLEID,LASTLOGIN,LASTPASSWORDCHANGED,LASTUPDATEDATE,ACTIVE,PHONENO,CREATEDBY FROM USERMASTER";
                //        ListUserIDs.Add((int)dgvUserCache.CurrentRow.Cells[dgvUserCache.Columns["USERID"].Index].Value);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Please Select an User To delete!", "Error");
                //        return;
                //    }
                //}
                //else
                //{
                //    foreach (DataGridViewRow ItemRow in dgvUserCache.Rows)
                //    {
                //        ListUserIDs.Add((int)ItemRow.Cells[dgvUserCache.Columns["USERID"].Index].Value);
                //    }

                //}
                #endregion
                //CheckOnce if one valid one invalid
                int CountOfSelected = 0;
                List<string> ListAlreadyDeletedUsers = new List<string>();
                foreach (DataGridViewRow ItemRow in dgvUserCache.Rows)
                {
                    //CheckBox chk = (CheckBox)ItemRow.Cells["checkBoxColumn"];
                    if (ItemRow.Cells["checkBoxColumn"].Value != null && ItemRow.Cells["checkBoxColumn"].Value.ToString().ToUpper() == "TRUE")
                    {
                        if (ItemRow.Cells["USERNAME"].Value.ToString().ToUpper() == "ADMIN")
                        {
                            ItemRow.Cells["checkBoxColumn"].Value = false;
                            MessageBox.Show("Admin login credential cant be deleted", "Error", MessageBoxButtons.OK);
                            continue;
                        }
                        if (ItemRow.Cells["ACTIVE"].Value.ToString().ToUpper() == "FALSE")
                        {
                            ListAlreadyDeletedUsers.Add(ItemRow.Cells["USERNAME"].Value.ToString());
                            //MessageBox.Show("The User is already InActive, To change the state pls use 'Edit User' section", "Error", MessageBoxButtons.OK);
                            // return;
                        }
                        else
                        {
                            ListUserIDs.Add((int)ItemRow.Cells[dgvUserCache.Columns["USERID"].Index].Value);
                            CountOfSelected++;
                        }
                      
                    }
                }
                if (ListAlreadyDeletedUsers.Count > 0)
                {
                    MessageBox.Show("The User/s  "+ string.Join(",",ListAlreadyDeletedUsers)+" already InActive, To change the state pls use 'Edit User' section", "Error", MessageBoxButtons.OK);
                }
                if (CountOfSelected == 0)
                {
                    if (ListAlreadyDeletedUsers.Count > 0) return;

                    MessageBox.Show("Please Select an User To delete!", "Error");
                    return;
                }

                var Result = MessageBox.Show("Are sure to set the User/s to Inactive State? ", "InActive User", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //If the no button was pressed ...
                if (Result == DialogResult.No) return;

                for (int i = 0; i < ListUserIDs.Count; i++)
                {
                    string WhereCondition = "USERID = '" + ListUserIDs[i] + "'";
                    List<string> ListColumnNames = new List<string>() { "ACTIVE" };
                    List<string> ListColumnValues = new List<string>() { "0" };
                    CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("USERMASTER", ListColumnNames, ListColumnValues, WhereCondition);
                }
                this.BindUserGrid(true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.btnRedirectDeleteUser_Click()", ex);
                throw;
            }
        }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            try
            {
                // CreateRoleForm ObjCreateRoleForm = new CreateRoleForm(this);
                // CommonFunctions.ShowDialog(ObjCreateRoleForm, this);
                // ObjCreateRoleForm.Show();
                CommonFunctions.ShowDialog(new CreateRoleForm(UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.btnCreateRole_Click()", ex);
                throw ex;
            }
        }
        void UpdateOnClose(Int32 Mode)
        {
            try
            {
                switch (Mode)
                {
                    case 1:     //Add Product
                        BindUserGrid(true);
                        //LoadUserDetailsDataGridView(true);
                        //if (!dgvUserCache.Controls.Contains(headerCheckBox))
                        //{
                        //    AddCheckBoxToDGV();
                        //}
                        break;
                    case 2:
                        CommonFunctions.ObjUserMasterModel.LoadAllUserMasterTables();
                        break;
                    //case 3:     //Reload Product Category from DB
                    //    LoadProductCategoryDataGridView(true);
                    //    break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUsersForm.UpdateOnClose()", ex);
            }
        }

        private void btnDefineRole_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new DefineRoleForm(UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.btnDefineRole_Click()", ex);
                throw ex;
            }
        }

        private void btnCreateStore_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateStoreForm(UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.btnCreateStore_Click()", ex);
                throw ex;
            }
        }

        private void btnEditStore_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new EditStoreForm(UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.btnEditStore_Click()", ex);
                throw ex;
            }
        }

        private void bgWorkerManageUser_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.bgWorkerManageUser_DoWork()", ex);
            }
          
        }

        private void bgWorkerManageUser_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUserForm.bgWorkerManageUser_RunWorkerCompleted()", ex);
            }
         
        }
    }
}
