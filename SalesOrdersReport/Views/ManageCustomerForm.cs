using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public delegate void UpdateOnCloseDel(Int32 Mode);
    public partial class ManageCustomerForm : Form
    {
        //public delegate void UpdateOnCloseDel(Int32 Mode);
        MySQLHelper tmpMySQlHelper = MySQLHelper.GetMySqlHelperObj();
        CheckBox headerCheckBox = new CheckBox();
        public ManageCustomerForm()
        {
            InitializeComponent();
        }
        private void btnRedirectCreateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                //CreateUserForm ObjCreateUserForm = new CreateUserForm(this);
                //ObjCreateUserForm.Show();
                //CommonFunctions.ShowDialog(ObjCreateUserForm,this);
                CommonFunctions.ShowDialog(new CreateCustomerForm(UpdateCustomerOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnRedirectCreateCustomer_Click()", ex);
                throw ex;
            }
        }

        private void btnRedirectEditCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                EditCustomerForm ObjEditCustomerForm = new EditCustomerForm(UpdateCustomerOnClose);

                DataGridViewRow CurrentRow = GetSelectedRowFrmGrid();
                if (CurrentRow == null) return;

                ObjEditCustomerForm.txtEditCustomerName.Text = CurrentRow.Cells["CUSTOMERNAME"].Value.ToString();
                ObjEditCustomerForm.txtEditCustAddress.Text = CurrentRow.Cells["ADDRESS"].Value.ToString();
                ObjEditCustomerForm.txtEditGSTIN.Text = CurrentRow.Cells["GSTIN"].Value.ToString();
                ObjEditCustomerForm.cmbxEditCustSelectLine.SelectedItem = CurrentRow.Cells["LINENAME"].Value.ToString();
                ObjEditCustomerForm.cmbxEditCustSelectPriceGrp.SelectedItem = CurrentRow.Cells["PRICEGROUPNAME"].Value.ToString();
                ObjEditCustomerForm.cmbxEditCustSelectDiscGrp.SelectedItem = CurrentRow.Cells["DISCOUNTGROUPNAME"].Value.ToString();
                ObjEditCustomerForm.cmbxEditCustSelectState.SelectedItem = CurrentRow.Cells["STATE"].Value.ToString();

                string[] OrderDaysCode = CurrentRow.Cells["ORDERDAYS"].Value.ToString().Split(',');
                string[] OrderDays = new string[OrderDaysCode.Length];
                for (int i = 0; i < OrderDaysCode.Length; i++)
                {
                    if (OrderDaysCode[i] != string.Empty) OrderDays[i] = CommonFunctions.ObjCustomerMasterModel.GetOrderDaysFromCode(int.Parse(OrderDaysCode[i]));
                    else OrderDays[i] = "";
                }
                for (int i = 0; i < OrderDays.Length; i++)
                {
                    foreach (CheckBox itemControl in ObjEditCustomerForm.flpEditCustOrderDays.Controls)
                    {
                        if (itemControl.Text.ToUpper() == OrderDays[i])
                        {
                            int gh = 0;
                            itemControl.Checked = true;
                        }
                    }
                }
                foreach (Control itemControl in ObjEditCustomerForm.flpEditCustOrderDays.Controls)
                {
                    CheckBox chbxControl = (CheckBox)(itemControl);
                    chbxControl.Checked = false;//set all the checbox un-checked at first
                    int index = Array.FindIndex(OrderDays, x => x.Equals(chbxControl.Text, StringComparison.InvariantCultureIgnoreCase));
                    if (index >= 0)
                    {
                        chbxControl.Checked = true;
                    }
                }

                //if (CurrentRow.Cells["CUSTOMERNAME"].Value.ToString().ToUpper() != "ADMIN")
                //{
                if (Convert.ToBoolean(CurrentRow.Cells["ACTIVE"].Value) == true)
                    ObjEditCustomerForm.rdbtnEditCustActiveYes.Checked = true;
                else ObjEditCustomerForm.rdbtnEditCustActiveNo.Checked = true;
                //}
                //else
                //{
                //    ObjEditCustomerForm.rdbtnEditCustActiveYes.Checked = true;
                //    ObjEditCustomerForm.rdbtnEditCustActiveNo.Enabled = false;
                //}
                ObjEditCustomerForm.txtEditCustPhone.Text = CurrentRow.Cells["PHONENO"].Value.ToString();
                if (ObjEditCustomerForm.txtEditCustPhone.Text == "0") ObjEditCustomerForm.txtEditCustPhone.Text = "";

                CommonFunctions.ShowDialog(ObjEditCustomerForm, this);


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
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnRedirectEditCustomer_Click()", ex);
                throw;
            }

        }

        private void btnReloadCustomerCache_Click(object sender, EventArgs e)
        {
            try
            {
                this.BindCustomerGrid(true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageCustomerForm.btnReloadCustomerCache_Click()", ex);
                throw ex;
            }
        }
        private void ManageCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                //EnableOrDisableBtnBasedOnUserPrivilege();
                this.BindCustomerGrid(false);//&&&&& should be true
                dgvCustomerCache.AutoResizeColumns();
                dgvCustomerCache.AutoResizeRows();
                dgvCustomerCache.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.ManageCustomer_Load()", ex);
                throw;
            }
        }
        public DataGridViewRow GetSelectedRowFrmGrid()
        {
            try
            {
                DataGridViewRow SelectedRow = null;
                int CountOfSelectedRow = 0;
                foreach (DataGridViewRow ItemRow in dgvCustomerCache.Rows)
                {
                    //CheckBox chk = (CheckBox)ItemRow.Cells["checkBoxColumn"];
                    if (ItemRow.Cells["checkBoxColumn"].Value != null && ItemRow.Cells["checkBoxColumn"].Value.ToString().ToUpper() == "TRUE")
                    {

                        CountOfSelectedRow++;
                        if (CountOfSelectedRow > 1)
                        {
                            MessageBox.Show("Please Select any one Customer To Edit!", "Error");
                            return null;
                        }
                        SelectedRow = ItemRow;
                    }
                }
                if (CountOfSelectedRow == 0)
                {
                    MessageBox.Show("Please Select an Customer To Edit!", "Error");
                    return null;
                }
                return SelectedRow;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.GetSelectedRowFrmGrid()", ex);
                throw;
            }


        }
        public void BindCustomerGrid(Boolean ReloadFromDB)
        {
            try
            {
                dgvCustomerCache.DataSource = null;
                dgvCustomerCache.Refresh();
                //dgvUserCache.DataSource = CommonFunctions.ObjUserMasterModel.FillMangeUserCacheGrid();
                Int32 ReturnVal = LoadCustomerDetailsDataGridView(ReloadFromDB);
                if (ReturnVal > 0)
                {
                    // if (!dgvUserCache.Controls.Contains(headerCheckBox))
                    // {
                    AddCheckBoxToDGV();
                    //}
                    EnableDisableControls(true);
                }
                else
                {
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("Message", typeof(string));
                    dataTable.Rows.Add("No Customer Added/found in DB");

                    dgvCustomerCache.DataSource = new BindingSource { DataSource = dataTable };
                    dgvCustomerCache.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    EnableDisableControls(false);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.BindCustomerGrid()", ex);
                throw;
            }
        }
        public void EnableDisableControls(bool Enable=true)
        {
            try
            {
                if (Enable)
                {
                    //btnCreateDiscountGrp.Enabled = true;
                    //btnEditDiscountGrp.Enabled = true;
                    //btnCreatePriceGrp.Enabled = true;
                    //btnEditPriceGrp.Enabled = true;
                    //btnRedirectCreateLine.Enabled = true;
                    //btnEditLine.Enabled = true;
                    btnRedirectEditCustomer.Enabled = true;
                    btnRedirectDeleteCustomer.Enabled = true;
                }
                else
                {
                    //btnCreateDiscountGrp.Enabled = false;
                    //btnEditDiscountGrp.Enabled = false;
                    //btnCreatePriceGrp.Enabled = false;
                    //btnEditPriceGrp.Enabled = false;
                    //btnRedirectCreateLine.Enabled = false;
                    //btnEditLine.Enabled = false;
                    btnRedirectEditCustomer.Enabled = false;
                    btnRedirectDeleteCustomer.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.BindCustomerGrid()", ex);
                throw;
            }
        }
        Int32 LoadCustomerDetailsDataGridView(Boolean ReloadFromDB)
        {
            try
            {
                if (ReloadFromDB) CommonFunctions.ObjCustomerMasterModel.LoadAllCustomerMasterTables();

                dgvCustomerCache.Rows.Clear();
                dgvCustomerCache.Columns.Clear();
                List<CustomerDetails> ListCustDtlsCache = CommonFunctions.ObjCustomerMasterModel.GetListCustomerCache();
                if (ListCustDtlsCache.Count > 0)
                {
                    String[] ArrColumnNames = new String[] { "CUSTOMERID", "LINEID", "DISCOUNTGROUPID", "PRICEGROUPID", "STATEID", "CUSTOMERNAME", "ADDRESS", "STATE", "PHONENO", "GSTIN", "ORDERDAYS", "LINENAME", "DISCOUNTGROUPNAME", "PRICEGROUPNAME", "ACTIVE", "ADDEDDATE", "LASTUPDATEDATE" };
                    String[] ArrColumnHeaders = new String[] { "CustomerID", "LineID", "Discount Group ID", "Price Group ID", "State ID", "Customer Name", "Address", "State", "Phone No", "GSTIN", "OrderDays", "Line", "Discount Group Name", "Price Group Name", "Active", "Added Date", "Last Update Date" };
                    for (int i = 0; i < ArrColumnNames.Length; i++)
                    {
                        dgvCustomerCache.Columns.Add(ArrColumnNames[i], ArrColumnHeaders[i]);
                        DataGridViewColumn CurrentCol = dgvCustomerCache.Columns[dgvCustomerCache.Columns.Count - 1];
                        CurrentCol.ReadOnly = true;
                        if (i <= 4) CurrentCol.Visible = false;    //CustomerID, LineID", "Discount Group ID, Price Group ID,State ID
                    }
                    foreach (CustomerDetails ObjCustomerDetails in ListCustDtlsCache)
                    {
                        Object[] ArrRowItems = new Object[17];
                        ArrRowItems[0] = ObjCustomerDetails.CustomerID;
                        ArrRowItems[1] = ObjCustomerDetails.LineID;
                        ArrRowItems[2] = ObjCustomerDetails.DiscountGroupID;
                        ArrRowItems[3] = ObjCustomerDetails.PriceGroupID;
                        ArrRowItems[4] = ObjCustomerDetails.StateID;
                        ArrRowItems[5] = ObjCustomerDetails.CustomerName;
                        ArrRowItems[6] = ObjCustomerDetails.Address;
                        ArrRowItems[7] = ObjCustomerDetails.State;
                        ArrRowItems[8] = ObjCustomerDetails.PhoneNo == 0 ? "" : ObjCustomerDetails.PhoneNo.ToString();
                        ArrRowItems[9] = ObjCustomerDetails.GSTIN;
                        ArrRowItems[10] = ObjCustomerDetails.OrderDaysAssigned;
                        ArrRowItems[11] = ObjCustomerDetails.LineName;
                        ArrRowItems[12] = ObjCustomerDetails.DiscountGroupName;
                        ArrRowItems[13] = ObjCustomerDetails.PriceGroupName;
                        ArrRowItems[14] = ObjCustomerDetails.Active;
                        ArrRowItems[15] = ObjCustomerDetails.AddedDate == DateTime.MinValue ? DateTime.Now : ObjCustomerDetails.AddedDate;
                        ArrRowItems[16] = ObjCustomerDetails.LastUpdateDate == DateTime.MinValue ? DateTime.Now : ObjCustomerDetails.LastUpdateDate;

                        dgvCustomerCache.Rows.Add(ArrRowItems);
                    }
                    return 1;
                }
                else return 0;     //no customers exists

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.LoadCustomerDetailsDataGridView()", ex);
                throw;
            }
        }

        private void AddCheckBoxToDGV()
        {
            try
            {
                //Add a CheckBox Column to the DataGridView Header Cell.

                //Find the Location of Header Cell.
                Point headerCellLocation = this.dgvCustomerCache.GetCellDisplayRectangle(5, -1, true).Location;

                //Place the Header CheckBox in the Location of the Header Cell.
                //headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
                headerCheckBox.Location = new Point(headerCellLocation.X + 2, headerCellLocation.Y + 3);
                headerCheckBox.BackColor = Color.White;
                headerCheckBox.Size = new Size(18, 18);
                if (headerCheckBox.Checked == true) headerCheckBox.Checked = false;
                //Assign Click event to the Header CheckBox.
                headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
                dgvCustomerCache.Controls.Add(headerCheckBox);

                //Add a CheckBox Column to the DataGridView at the first position.
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "";
                checkBoxColumn.Width = 30;
                checkBoxColumn.Name = "checkBoxColumn";
                dgvCustomerCache.Columns.Insert(4, checkBoxColumn);

                //Assign Click event to the DataGridView Cell.
                dgvCustomerCache.CellContentClick += new DataGridViewCellEventHandler(CustomerDataGridView_CellClick);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.AddCheckBoxToDGV()", ex);
                throw;
            }

        }
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Necessary to end the edit mode of the Cell.
                dgvCustomerCache.EndEdit();

                //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
                foreach (DataGridViewRow row in dgvCustomerCache.Rows)
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
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.HeaderCheckBox_Clicked()", ex);
                throw;
            }
        }

        private void CustomerDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Check to ensure that the row CheckBox is clicked.
                if (e.RowIndex >= 0 && e.ColumnIndex == dgvCustomerCache.Columns["checkBoxColumn"].Index)
                {
                    //Loop to verify whether all row CheckBoxes are checked or not.
                    bool isChecked = true;
                    foreach (DataGridViewRow row in dgvCustomerCache.Rows)
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
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.CustomerDataGridView_CellClick()", ex);
                throw;
            }
        }

        private void btnRedirectDeleteCustomer_Click(object sender, EventArgs e)
        {

            try
            {
                List<int> ListCustomerIDs = new List<int>();
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
                List<string> ListAlreadyDeletedCustomer = new List<string>();
                foreach (DataGridViewRow ItemRow in dgvCustomerCache.Rows)
                {
                    //CheckBox chk = (CheckBox)ItemRow.Cells["checkBoxColumn"];
                    if (ItemRow.Cells["checkBoxColumn"].Value != null && ItemRow.Cells["checkBoxColumn"].Value.ToString().ToUpper() == "TRUE")
                    {
                        //if (ItemRow.Cells["CUSTOMERNAME"].Value.ToString().ToUpper() == "ADMIN")
                        //{
                        //    ItemRow.Cells["checkBoxColumn"].Value = false;
                        //    MessageBox.Show("Admin login credential cant be deleted", "Error", MessageBoxButtons.OK);
                        //    continue;
                        //}
                        if (ItemRow.Cells["ACTIVE"].Value.ToString().ToUpper() == "FALSE")
                        {
                            ListAlreadyDeletedCustomer.Add(ItemRow.Cells["CUSTOMERNAME"].Value.ToString());
                            //MessageBox.Show("The User is already InActive, To change the state pls use 'Edit User' section", "Error", MessageBoxButtons.OK);
                            // return;
                        }
                        else
                        {
                            ListCustomerIDs.Add((int)ItemRow.Cells[dgvCustomerCache.Columns["CUSTOMERID"].Index].Value);
                            CountOfSelected++;
                        }

                    }
                }
                if (ListAlreadyDeletedCustomer.Count > 0)
                {
                    MessageBox.Show("The Customer/s  " + string.Join(",", ListAlreadyDeletedCustomer) + " already InActive, To change the state pls use 'Edit Customer' section", "Error", MessageBoxButtons.OK);
                }
                if (CountOfSelected == 0)
                {
                    if (ListAlreadyDeletedCustomer.Count > 0) return;

                    MessageBox.Show("Please Select an Customer To delete!", "Error");
                    return;
                }

                var Result = MessageBox.Show("Are sure to set the Customer/s to Inactive State? ", "InActive Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //If the no button was pressed ...
                if (Result == DialogResult.No) return;

                for (int i = 0; i < ListCustomerIDs.Count; i++)
                {
                    string WhereCondition = "CUSTOMERID = '" + ListCustomerIDs[i] + "'";
                    List<string> ListColumnNames = new List<string>() { "ACTIVE" };
                    List<string> ListColumnValues = new List<string>() { "0" };
                    CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("CUSTOMERMASTER", ListColumnNames, ListColumnValues, WhereCondition);
                }
                this.BindCustomerGrid(true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnRedirectDeleteCustomer_Click()", ex);
                throw;
            }
        }

        private void btnCreateLine_Click(object sender, EventArgs e)
        {
            try
            {
                // CreateRoleForm ObjCreateRoleForm = new CreateRoleForm(this);
                // CommonFunctions.ShowDialog(ObjCreateRoleForm, this);
                // ObjCreateRoleForm.Show();
                CommonFunctions.ShowDialog(new CreateLineForm(UpdateCustomerOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnCreateLine_Click()", ex);
                throw ex;
            }
        }
        void UpdateCustomerOnClose(Int32 Mode)
        {
            try
            {
                switch (Mode)
                {
                    case 1:     //Add Product
                        BindCustomerGrid(true);
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

        private void btnEditLine_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new EditLineForm(UpdateCustomerOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnEditLine_Click()", ex);
                throw ex;
            }
        }

        private void btnCreatePriceGrp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreatePriceGroupForm(UpdateCustomerOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnCreatePriceGrp_Click()", ex);
                throw ex;
            }
        }

        private void btnEditPriceGrp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new EditPriceGroupForm(UpdateCustomerOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnEditPriceGrp_Click()", ex);
                throw ex;
            }
        }


        private void btnEditDiscountGrp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new EditDiscountGroupForm(UpdateCustomerOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnEditDiscountGrp_Click()", ex);
                throw ex;
            }
        }

        private void btnCreateDiscountGrp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateDiscountGroupForm(UpdateCustomerOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnDiscountGrp_Click()", ex);
                throw ex;
            }
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ImportFromExcelForm(IMPORTDATATYPES.CUSTOMERS, UpdateCustomerOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnImportFromExcel_Click()", ex);
                throw ex;
            }
        }
    }
}
