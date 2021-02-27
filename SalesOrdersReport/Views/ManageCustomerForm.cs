using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public partial class ManageCustomerForm : Form
    {
        MySQLHelper tmpMySQlHelper = MySQLHelper.GetMySqlHelperObj();
        CheckBox headerCheckBox = new CheckBox();
        public ManageCustomerForm()
        {
            try
            {
            InitializeComponent();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.ManageCustomerForm()", ex);
                throw ex;
            }
        }
        private void btnRedirectCreateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
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
                    if (OrderDaysCode[i] != string.Empty) OrderDays[i] = CommonFunctions.ObjCustomerMasterModel.GetOrderDaysFromCode(int.Parse(OrderDaysCode[i].Replace('"',' ').Trim()));
                    else OrderDays[i] = "";
                }
                for (int i = 0; i < OrderDays.Length; i++)
                {
                    foreach (CheckBox itemControl in ObjEditCustomerForm.flpEditCustOrderDays.Controls)
                    {
                        if (itemControl.Text.ToUpper() == OrderDays[i])
                        {
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

                if (Convert.ToBoolean(CurrentRow.Cells["ACTIVE"].Value) == true)
                    ObjEditCustomerForm.rdbtnEditCustActiveYes.Checked = true;
                else ObjEditCustomerForm.rdbtnEditCustActiveNo.Checked = true;

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
                this.BindCustomerGrid(false);
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
                    //EnableDisableControls(true);
                }
                else
                {
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("Message", typeof(string));
                    dataTable.Rows.Add("No Customer Added/found in DB");

                    dgvCustomerCache.DataSource = new BindingSource { DataSource = dataTable };
                    dgvCustomerCache.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    //EnableDisableControls(false);
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
                    btnCreateDiscountGrp.Enabled = true;
                    btnEditDiscountGrp.Enabled = true;
                    btnCreatePriceGrp.Enabled = true;
                    btnEditPriceGrp.Enabled = true;
                    btnRedirectCreateLine.Enabled = true;
                    btnEditLine.Enabled = true;
                    btnRedirectEditCustomer.Enabled = true;
                    btnRedirectDeleteCustomer.Enabled = true;
                }
                else
                {
                    btnEditDiscountGrp.Enabled = false;
                    btnEditPriceGrp.Enabled = false;
                    btnEditLine.Enabled = false;
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
                    case 1:     
                        BindCustomerGrid(true);
                        break;
                    case 2:
                        CommonFunctions.ObjCustomerMasterModel.LoadAllCustomerMasterTables();
                        break;
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
                CommonFunctions.ShowDialog(new ImportFromExcelForm(ImportDataTypes.Customers, UpdateCustomerOnClose, ReadNProcessCustomerFile), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerCustomerForm.btnImportFromExcel_Click()", ex);
                throw ex;
            }
        }

        private Int32 ReadNProcessCustomerFile(String FilePathToImport, Object ObjDetails, ReportProgressDel ReportProgress)
        {
            try
            {
                List<CustomerDetails> ListTempCustDtls = new List<CustomerDetails>();
                List<LineDetails> ListTempLineDtls = new List<LineDetails>();
                List<PriceGroupDetails> ListTempPGDtls = new List<PriceGroupDetails>();
                List<DiscountGroupDetails1> ListTempDGDtls = new List<DiscountGroupDetails1>();

                int LastLineIDFromDB = CommonFunctions.ObjCustomerMasterModel.GetLatestColValFromTable("LINEID", "LINEMASTER");
                int LastPGIDFromDB = CommonFunctions.ObjCustomerMasterModel.GetLatestColValFromTable("PRICEGROUPID", "PRICEGROUPMASTER");
                int LastDGIDFromDB = CommonFunctions.ObjCustomerMasterModel.GetLatestColValFromTable("DISCOUNTGROUPID", "DISCOUNTGROUPMASTER");
                int LastCustIDFromDB = CommonFunctions.ObjCustomerMasterModel.GetLatestColValFromTable("CUSTOMERID", "CUSTOMERMASTER");
                List<string> ListExistingLineNamesinDB = CommonFunctions.ObjCustomerMasterModel.GetAllLineNames();
                List<string> ListExistingDiscGrpNamesinDB = CommonFunctions.ObjCustomerMasterModel.GetAllDiscGrp();
                List<string> ListExistingPriceGrpNamesinDB = CommonFunctions.ObjCustomerMasterModel.GetAllPriceGrp();
                List<string> ListExistingCustomerNamesinDB = CommonFunctions.ObjCustomerMasterModel.GetCustomerList();

                ImportFromExcelCustomerCache ObjImportFromExcelCustomerCache = new ImportFromExcelCustomerCache();
                StreamReader srReadSelectedFile = new StreamReader(FilePathToImport);
                string HeaderStr = srReadSelectedFile.ReadLine();
                char ColSeparator = CommonFunctions.GetColSeparator(HeaderStr);
                string[] Header = HeaderStr.Split(ColSeparator);
                //CustomerName,LineName,PriceGroupName,DiscountGroupName,State,Default,Active,Address,GSTIN,AddedDate,LastUpdateDate,Phone,OrderDays
                int CustomerNameIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.CustomerColName, StringComparison.InvariantCultureIgnoreCase));
                int LineNameIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.LineColName, StringComparison.InvariantCultureIgnoreCase));
                int PriceGroupNameIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.PriceGroupColName, StringComparison.InvariantCultureIgnoreCase));
                int DiscountGroupNameIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.DiscountGroupColName, StringComparison.InvariantCultureIgnoreCase));
                int StateIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.StateColName, StringComparison.InvariantCultureIgnoreCase));
                int PGDefaultIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.PGDefaultColName, StringComparison.InvariantCultureIgnoreCase));
                int DGDefaultIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.DGDefaultColName, StringComparison.InvariantCultureIgnoreCase));
                int ActiveIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.ActiveColName, StringComparison.InvariantCultureIgnoreCase));
                int AddressIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.AddressColName, StringComparison.InvariantCultureIgnoreCase));
                int SelectedPriceGroupColNameIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.SelectedPriceGrpColName, StringComparison.InvariantCultureIgnoreCase));
                int GSTINIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.GSTINColName, StringComparison.InvariantCultureIgnoreCase));
                int AddedDateIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.AddedDateColName, StringComparison.InvariantCultureIgnoreCase));
                int LastUpdateDateIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.LastUpdateDateColName, StringComparison.InvariantCultureIgnoreCase));

                int PG_DiscountTypeIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.PGDiscCountTypeColName, StringComparison.InvariantCultureIgnoreCase));
                int DG_DiscountTypeIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.DGDiscCountTypeColName, StringComparison.InvariantCultureIgnoreCase));

                int PhoneIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.PhoneNoColName, StringComparison.InvariantCultureIgnoreCase));
                int OrderDaysIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.OrderDaysColName, StringComparison.InvariantCultureIgnoreCase));

                int CountOfDisctinctCustomers = 0;
                List<string> ListOfCustAlreadyInDB = new List<string>();

                int LineID = LastLineIDFromDB + 1, PGID = LastPGIDFromDB + 1, DGID = LastDGIDFromDB + 1, CustID = LastCustIDFromDB + 1;
                if (LineID == 0) LineID += 1;
                if (PGID == 0) PGID += 1;
                if (DGID == 0) DGID += 1;
                if (CustID == 0) CustID += 1;
                while (srReadSelectedFile.Peek() != -1)
                {
                    string[] arr = srReadSelectedFile.ReadLine().Split(ColSeparator);
                    CustomerDetails ObjCD = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(arr[CustomerNameIndex].Trim());
                    if (ObjCD != null)
                    {
                        ListOfCustAlreadyInDB.Add(arr[CustomerNameIndex]);
                        continue;
                    }
                    CountOfDisctinctCustomers++;
                    CustomerDetails ObjCustomerDetails = new CustomerDetails();
                    ObjCustomerDetails.CustomerName = arr[CustomerNameIndex].Trim();
                    ObjCustomerDetails.Address = arr[AddressIndex].Trim();
                    ObjCustomerDetails.GSTIN = arr[GSTINIndex].Trim();
                    ObjCustomerDetails.PhoneNo = ((arr[PhoneIndex] == null) || arr[PhoneIndex].Trim() == "") ? 0 : Int64.Parse(arr[PhoneIndex].Trim());
                    if (arr[ActiveIndex].Trim() != "") ObjCustomerDetails.Active = bool.Parse(arr[ActiveIndex].Trim());
                    ObjCustomerDetails.State = ((arr[StateIndex] == null) || arr[StateIndex].Trim() == "") ? "" : arr[StateIndex].Trim();
                    ObjCustomerDetails.OrderDaysAssigned = arr[OrderDaysIndex].Replace('"',' ').Trim();
                    ObjCustomerDetails.LineName = ((arr[LineNameIndex] == null) || arr[LineNameIndex].Trim() == "") ? "" : arr[LineNameIndex].Trim();
                    ObjCustomerDetails.DiscountGroupName = ((arr[DiscountGroupNameIndex] == null) || arr[DiscountGroupNameIndex].Trim() == "") ? "" : arr[DiscountGroupNameIndex].Trim();
                    ObjCustomerDetails.PriceGroupName = ((arr[PriceGroupNameIndex] == null) || arr[PriceGroupNameIndex].Trim() == "") ? "" : arr[PriceGroupNameIndex].Trim();
                    int Index = -1, AlreadyExistsIndex = -1;

                    AlreadyExistsIndex = ListExistingLineNamesinDB.FindIndex(e => e.Equals(ObjCustomerDetails.LineName, StringComparison.InvariantCultureIgnoreCase));
                    if (AlreadyExistsIndex < 0)
                    {
                        LineDetails ObjLineDetails = new LineDetails();
                        ObjLineDetails.LineName = ObjCustomerDetails.LineName;
                        Index = ListTempLineDtls.BinarySearch(ObjLineDetails, ObjLineDetails);
                        if (Index < 0)
                        {
                            ObjLineDetails.LineID = ObjCustomerDetails.LineID = LineID;
                            ListTempLineDtls.Insert(~Index, ObjLineDetails);
                            LineID++;
                        }
                    }
                    else ObjCustomerDetails.LineID = CommonFunctions.ObjCustomerMasterModel.GetLineID(ListExistingLineNamesinDB[AlreadyExistsIndex]);

                    AlreadyExistsIndex = ListExistingDiscGrpNamesinDB.FindIndex(e => e.Equals(ObjCustomerDetails.DiscountGroupName, StringComparison.InvariantCultureIgnoreCase));
                    if (AlreadyExistsIndex < 0)
                    {
                        DiscountGroupDetails1 ObjDiscountGroupDetails = new DiscountGroupDetails1();
                        ObjDiscountGroupDetails.DiscountGrpName = ObjCustomerDetails.DiscountGroupName;
                        Index = ListTempDGDtls.BinarySearch(ObjDiscountGroupDetails, ObjDiscountGroupDetails);
                        if (Index < 0)
                        {
                            ObjDiscountGroupDetails.DiscountGrpID = ObjCustomerDetails.DiscountGroupID = DGID;
                            ObjDiscountGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(arr[DG_DiscountTypeIndex].Trim());
                            ObjDiscountGroupDetails.IsDefault = bool.Parse(arr[DGDefaultIndex].Trim());
                            if (arr[DGDefaultIndex].Trim() != "") ListTempDGDtls.Insert(~Index, ObjDiscountGroupDetails);
                            DGID++;
                        }
                    }
                    else ObjCustomerDetails.DiscountGroupID = CommonFunctions.ObjCustomerMasterModel.GetDisGrpID(ListExistingDiscGrpNamesinDB[AlreadyExistsIndex]);
                    AlreadyExistsIndex = ListExistingPriceGrpNamesinDB.FindIndex(e => e.Equals(ObjCustomerDetails.PriceGroupName, StringComparison.InvariantCultureIgnoreCase));
                    if (AlreadyExistsIndex < 0)
                    {
                        PriceGroupDetails ObjPriceGroupDetails = new PriceGroupDetails();
                        ObjPriceGroupDetails.PriceGrpName = ObjCustomerDetails.PriceGroupName;
                        Index = ListTempPGDtls.BinarySearch(ObjPriceGroupDetails, ObjPriceGroupDetails);
                        if (Index < 0)
                        {
                            ObjPriceGroupDetails.PriceGroupID = ObjCustomerDetails.PriceGroupID = PGID;
                            ObjPriceGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(arr[PG_DiscountTypeIndex].Trim());
                            ObjPriceGroupDetails.IsDefault = bool.Parse(arr[PGDefaultIndex].Trim());
                            if (arr[PGDefaultIndex].Trim() != "") ObjPriceGroupDetails.PriceColumn = arr[SelectedPriceGroupColNameIndex].Trim();
                            ListTempPGDtls.Insert(~Index, ObjPriceGroupDetails);
                            PGID++;
                        }
                    }
                    else ObjCustomerDetails.PriceGroupID = CommonFunctions.ObjCustomerMasterModel.GetPriceGrpID(ListExistingPriceGrpNamesinDB[AlreadyExistsIndex]);
                    AlreadyExistsIndex = ListExistingCustomerNamesinDB.FindIndex(e => e.Equals(ObjCustomerDetails.CustomerName, StringComparison.InvariantCultureIgnoreCase));
                    if (AlreadyExistsIndex < 0)
                    {
                        Index = ListTempCustDtls.BinarySearch(ObjCustomerDetails, ObjCustomerDetails);
                        if (Index < 0)
                        {
                            ObjCustomerDetails.CustomerID = CustID;
                            ObjCustomerDetails.StateID = CommonFunctions.ObjCustomerMasterModel.GetStateID(ObjCustomerDetails.State);
                            ListTempCustDtls.Insert(~Index, ObjCustomerDetails);
                            CustID++;
                        }
                    }
                }
                srReadSelectedFile.Close();

                if (ListTempLineDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.FillLineDBFromCache(ListTempLineDtls);
                if (ListTempDGDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.FillDiscountGroupDBFromCache(ListTempDGDtls);
                if (ListTempPGDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.FillPriceGroupDBFromCache(ListTempPGDtls);
                if (ListTempCustDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.FillCustomerDBFromCache(ListTempCustDtls);

                if (ListTempLineDtls.Count > 0 || ListTempDGDtls.Count > 0 || ListTempPGDtls.Count > 0 || ListTempCustDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.LoadAllCustomerMasterTables();

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ReadNProcessCustomerFile()", ex);
                return -1;
            }
        }

        private void ManageCustomerForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ManageCustomerForm_Shown()", ex);
            }
        }
    }
}
