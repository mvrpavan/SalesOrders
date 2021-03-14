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
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesOrdersReport.Views
{
    public partial class ManageVendorForm : Form
    {
        MySQLHelper tmpMySQlHelper = MySQLHelper.GetMySqlHelperObj();
        CheckBox headerCheckBox = new CheckBox();

        DataTable dtAllVendors;
        public ManageVendorForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.ManageVendorForm()", ex);
                throw ex;
            }
        }
   
        private void btnRedirectCreateVendor_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateVendorForm(UpdateVendorOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.btnRedirectCreateVendor_Click()", ex);
                throw ex;
            }
        }

        private void btnRedirectEditVendor_Click(object sender, EventArgs e)
        {
            try
            {
                EditVendorForm ObjEditVendorForm = new EditVendorForm(UpdateVendorOnClose);

                DataGridViewRow CurrentRow = GetSelectedRowFrmGrid();
                if (CurrentRow == null) return;

                ObjEditVendorForm.txtEditVendorName.Text = CurrentRow.Cells["VendorName"].Value.ToString();
                ObjEditVendorForm.txtEditVendorAddress.Text = CurrentRow.Cells["Address"].Value.ToString();
                ObjEditVendorForm.txtEditGSTIN.Text = CurrentRow.Cells["GSTIN"].Value.ToString();
                ObjEditVendorForm.cmbxEditVendorSelectState.SelectedItem = CurrentRow.Cells["State"].Value.ToString();


                if (Convert.ToBoolean(CurrentRow.Cells["Active"].Value) == true)
                    ObjEditVendorForm.rdbtnEditVendorActiveYes.Checked = true;
                else ObjEditVendorForm.rdbtnEditVendorActiveNo.Checked = true;

                ObjEditVendorForm.txtEditVendorPhone.Text = CurrentRow.Cells["PhoneNo"].Value.ToString();
                if (ObjEditVendorForm.txtEditVendorPhone.Text == "0") ObjEditVendorForm.txtEditVendorPhone.Text = "";

                CommonFunctions.ShowDialog(ObjEditVendorForm, this);

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.btnRedirectEditVendor_Click()", ex);
                throw;
            }

        }

        private void btnReloadVendorCache_Click(object sender, EventArgs e)
        {
            try
            {
                this.BindVendorGrid(true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageVendorForm.btnReloadVendorCache_Click()", ex);
                throw ex;
            }
        }
        private void ManageVendor_Load(object sender, EventArgs e)
        {
            try
            {
                //EnableOrDisableBtnBasedOnUserPrivilege();
                this.BindVendorGrid(true);
                dgvVendorCache.AutoResizeColumns();
                dgvVendorCache.AutoResizeRows();
                dgvVendorCache.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.ManageVendor_Load()", ex);
                throw;
            }
        }
        public DataGridViewRow GetSelectedRowFrmGrid()
        {
            try
            {
                DataGridViewRow SelectedRow = null;
                int CountOfSelectedRow = 0;
                foreach (DataGridViewRow ItemRow in dgvVendorCache.Rows)
                {
                    //CheckBox chk = (CheckBox)ItemRow.Cells["checkBoxColumn"];
                    if (ItemRow.Cells["checkBoxColumn"].Value != null && ItemRow.Cells["checkBoxColumn"].Value.ToString().ToUpper() == "TRUE")
                    {
                        CountOfSelectedRow++;
                        if (CountOfSelectedRow > 1)
                        {
                            MessageBox.Show("Please Select any one Vendor To Edit!", "Error");
                            return null;
                        }
                        SelectedRow = ItemRow;
                    }
                }
                if (CountOfSelectedRow == 0)
                {
                    MessageBox.Show("Please Select an Vendor To Edit!", "Error");
                    return null;
                }
                return SelectedRow;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.GetSelectedRowFrmGrid()", ex);
                throw;
            }
        }
        public void BindVendorGrid(Boolean ReloadFromDB)
        {
            try
            {
                dgvVendorCache.DataSource = null;
                dgvVendorCache.Refresh();
                LoadVendorDetailsDataGridView(ReloadFromDB);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.BindVendorGrid()", ex);
                throw;
            }
        }
        public void EnableDisableControls(bool Enable = true)
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
                    btnRedirectEditVendor.Enabled = true;
                    btnRedirectDeleteVendor.Enabled = true;
                }
                else
                {
                    //btnEditDiscountGrp.Enabled = false;
                    //btnEditPriceGrp.Enabled = false;
                    //btnEditLine.Enabled = false;
                    btnRedirectEditVendor.Enabled = false;
                    btnRedirectDeleteVendor.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.BindVendorGrid()", ex);
                throw;
            }
        }
        void LoadVendorDetailsDataGridView(Boolean ReloadFromDB)
        {
            try
            {
                if (ReloadFromDB) dtAllVendors = CommonFunctions.ObjVendorMaster.LoadNGetVendorDataTable();

                dgvVendorCache.DataSource = null;
                if (dgvVendorCache.Columns.Count > 0) dgvVendorCache.Columns.Clear();
                if (dtAllVendors == null || dtAllVendors.Rows.Count == 0)
                {
                    dgvVendorCache.DataSource = new BindingSource { DataSource = CommonFunctions.GetDataTableWhenNoRecordsFound() };
                    dgvVendorCache.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    return;
                }
                LoadGridView(dtAllVendors);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.LoadVendorDetailsDataGridView()", ex);
                throw;
            }
        }

        void LoadGridView(DataTable DtNew)
        {
            try
            {
                String[] ArrColumnNames = new String[] { "VendorID", "StateID", "VendorName", "Address", "State", "PhoneNo", "GSTIN", "Active", "AddedDate", "LastUpdateDate" };
                String[] ArrColumnHeaders = new String[] { "VendorID", "State ID", "Vendor Name", "Address", "State", "Phone No", "GSTIN", "Active", "Added Date", "Last Update Date" };
                for (int i = 0; i < ArrColumnNames.Length; i++)
                {
                    dgvVendorCache.Columns.Add(ArrColumnNames[i], ArrColumnHeaders[i]);
                    DataGridViewColumn CurrentCol = dgvVendorCache.Columns[dgvVendorCache.Columns.Count - 1];
                    CurrentCol.ReadOnly = true;
                    if (i <= 1) CurrentCol.Visible = false;    //VendorID,State ID

                }
                for (int i = 0; i < DtNew.Rows.Count; i++)
                {
                    Object[] ArrRowItems = new Object[ArrColumnNames.Length];
                    DataRow dr = DtNew.Rows[i];
                    int col = 0;
                    ArrRowItems[col++] = dr["VendorID"];
                    ArrRowItems[col++] = dr["StateID"]; ;
                    ArrRowItems[col++] = dr["VendorName"];
                    ArrRowItems[col++] = dr["Address"]; ;
                    ArrRowItems[col++] = (dr["StateID"].ToString() == "") ? "" : CommonFunctions.ObjCustomerMasterModel.GetStateName(int.Parse(dr["StateID"].ToString())).ToString();
                    ArrRowItems[col++] = dr["PhoneNo"];
                    ArrRowItems[col++] = dr["GSTIN"];
                    ArrRowItems[col++] = dr["Active"];
                    ArrRowItems[col++] = dr["AddedDate"];
                    ArrRowItems[col++] = dr["LastUpdateDate"];

                    dgvVendorCache.Rows.Add(ArrRowItems);
                }
                AddCheckBoxToDGV();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AddCheckBoxToDGV()
        {
            try
            {
                //Add a CheckBox Column to the DataGridView Header Cell.

                //Find the Location of Header Cell.
                Point headerCellLocation = this.dgvVendorCache.GetCellDisplayRectangle(2, -1, true).Location;

                //Place the Header CheckBox in the Location of the Header Cell.
                //headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
                headerCheckBox.Location = new Point(headerCellLocation.X + 2, headerCellLocation.Y + 14);
                headerCheckBox.BackColor = Color.White;
                headerCheckBox.Size = new Size(18, 18);
                if (headerCheckBox.Checked == true) headerCheckBox.Checked = false;
                //Assign Click event to the Header CheckBox.
                headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
                dgvVendorCache.Controls.Add(headerCheckBox);

                //Add a CheckBox Column to the DataGridView at the position.
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "";
                checkBoxColumn.Width = 30;
                checkBoxColumn.Name = "checkBoxColumn";
                dgvVendorCache.Columns.Insert(2, checkBoxColumn);

                //Assign Click event to the DataGridView Cell.
                dgvVendorCache.CellContentClick += new DataGridViewCellEventHandler(VendorDataGridView_CellClick);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.AddCheckBoxToDGV()", ex);
                throw;
            }

        }
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Necessary to end the edit mode of the Cell.
                dgvVendorCache.EndEdit();

                //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
                foreach (DataGridViewRow row in dgvVendorCache.Rows)
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
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.HeaderCheckBox_Clicked()", ex);
                throw;
            }
        }

        private void VendorDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Check to ensure that the row CheckBox is clicked.
                if (e.RowIndex >= 0 && e.ColumnIndex == dgvVendorCache.Columns["checkBoxColumn"].Index)
                {
                    //Loop to verify whether all row CheckBoxes are checked or not.
                    bool isChecked = true;
                    foreach (DataGridViewRow row in dgvVendorCache.Rows)
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
                    }
                    headerCheckBox.Checked = isChecked;

                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.VendorDataGridView_CellClick()", ex);
                throw;
            }
        }

        private void btnRedirectDeleteVendor_Click(object sender, EventArgs e)
        {

            try
            {
                List<int> ListVendorIDs = new List<int>();
                //CheckOnce if one valid one invalid
                int CountOfSelected = 0;
                List<string> ListAlreadyDeletedVendor = new List<string>();
                foreach (DataGridViewRow ItemRow in dgvVendorCache.Rows)
                {
                    if (ItemRow.Cells["checkBoxColumn"].Value != null && ItemRow.Cells["checkBoxColumn"].Value.ToString().ToUpper() == "TRUE")
                    {
                        if (ItemRow.Cells["Active"].Value.ToString().ToUpper() == "FALSE")
                        {
                            ListAlreadyDeletedVendor.Add(ItemRow.Cells["VendorName"].Value.ToString());
                        }
                        else
                        {
                            ListVendorIDs.Add((int)ItemRow.Cells[dgvVendorCache.Columns["VendorID"].Index].Value);
                            CountOfSelected++;
                        }
                    }
                }
                if (ListAlreadyDeletedVendor.Count > 0)
                {
                    MessageBox.Show("The Vendor/s  " + string.Join(",", ListAlreadyDeletedVendor) + " already InActive, To change the state pls use 'Edit Vendor' section", "Error", MessageBoxButtons.OK);
                }
                if (CountOfSelected == 0)
                {
                    if (ListAlreadyDeletedVendor.Count > 0) return;

                    MessageBox.Show("Please Select an Vendor To delete!", "Error");
                    return;
                }

                var Result = MessageBox.Show("Are sure to set the Vendor/s to Inactive State? ", "InActive Vendor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //If the no button was pressed ...
                if (Result == DialogResult.No) return;

                for (int i = 0; i < ListVendorIDs.Count; i++)
                {
                    string WhereCondition = "VendorID = '" + ListVendorIDs[i] + "'";
                    List<string> ListColumnNames = new List<string>() { "Active" };
                    List<string> ListColumnValues = new List<string>() { "0" };
                    CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("VendorMaster", ListColumnNames, ListColumnValues, WhereCondition);
                }
                this.BindVendorGrid(true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.btnRedirectDeleteVendor_Click()", ex);
                throw;
            }
        }

        private void btnCreateLine_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateLineForm(UpdateVendorOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.btnCreateLine_Click()", ex);
                throw ex;
            }
        }
        void UpdateVendorOnClose(Int32 Mode)
        {
            try
            {
                switch (Mode)
                {
                    case 1:
                        BindVendorGrid(true);
                        break;
                    case 2:
                        CommonFunctions.ObjVendorMaster.LoadNGetVendorDataTable();
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
        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ImportFromExcelForm(ImportDataTypes.Vendors, UpdateVendorOnClose, ProcessVendorRelatedFile), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.btnImportFromExcel_Click()", ex);
                throw ex;
            }
        }
        private Int32 ProcessVendorRelatedFile(String FilePathToImport, Object ObjDetails, ReportProgressDel ReportProgress)
        {
            try
            {
                List<VendorDetails> ListTempVendorDtls = new List<VendorDetails>();

                MySQLHelper ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                int LastVendorIDFromDB = ObjMySQLHelper.GetLatestColValFromTable("VendorID", "VendorMaster");

                List<string> ListExistingVendorNamesinDB = CommonFunctions.ObjVendorMaster.GetVendorList();
                List<string> ListOfCustAlreadyInDB = new List<string>();

 
                int VendorID = LastVendorIDFromDB + 1;

                if (VendorID == 0) VendorID += 1;

                Excel.Application xlApp = new Excel.Application();

                DataTable dtVendorDetails = CommonFunctions.ReturnDataTableFromExcelWorksheet("Vendor Details", FilePathToImport, "*");
                if (dtVendorDetails == null)
                {
                    MessageBox.Show(this, "Provided Vendor details file doesn't contain \"Vendor Details\" Sheet.\nPlease provide correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                ImportFromExcelVendorCache ObjImportFromExcelVendorCache = new ImportFromExcelVendorCache();
                int AlreadyExistsIndex = -1;
	
                List<string> ListErrMsg = new List<string>();
                foreach (DataRow item in dtVendorDetails.Rows)
                {
                    VendorDetails ObjVendorDetails = new VendorDetails();
                    ObjVendorDetails.VendorName = item[ObjImportFromExcelVendorCache.VendorColName].ToString().Trim();
                    AlreadyExistsIndex = ListExistingVendorNamesinDB.FindIndex(e => e.Equals(ObjVendorDetails.VendorName, StringComparison.InvariantCultureIgnoreCase));
                    if (AlreadyExistsIndex < 0)
                    {
                        int Index = ListTempVendorDtls.BinarySearch(ObjVendorDetails, ObjVendorDetails);
                        if (Index < 0)
                        {
                            ObjVendorDetails.VendorID = VendorID;

                            ObjVendorDetails.State = item[ObjImportFromExcelVendorCache.StateColName].ToString().Trim();
                            ObjVendorDetails.StateID = CommonFunctions.ObjCustomerMasterModel.GetStateID(ObjVendorDetails.State);
                            ObjVendorDetails.Active = (item[ObjImportFromExcelVendorCache.ActiveColName].ToString().Trim().ToUpper() == "TRUE") ? true : false;

                            ObjVendorDetails.Address = item[ObjImportFromExcelVendorCache.AddressColName].ToString().Trim();
                            ObjVendorDetails.GSTIN = item[ObjImportFromExcelVendorCache.GSTINColName].ToString().Trim();
                            ObjVendorDetails.PhoneNo = item[ObjImportFromExcelVendorCache.PhoneNoColName].ToString().Trim();

                            ListTempVendorDtls.Insert(~Index, ObjVendorDetails);
                            VendorID++;
                        }
                    }

                }

                if (ListErrMsg.Count > 0) MessageBox.Show("Following are Error Msgs " + string.Join("\n", ListErrMsg), "Missed Vendors", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (ListTempVendorDtls.Count > 0)
                {
                    CommonFunctions.ObjVendorMaster.FillVendorDBFromCache(ListTempVendorDtls);
                    CommonFunctions.ObjVendorMaster.LoadNGetVendorDataTable();
                }
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManagerVendorForm.ProcessVendorRelatedFile()", ex);
                throw ex;
            }
        }
        private Int32 ReadNProcessVendorFile(String FilePathToImport, Object ObjDetails, ReportProgressDel ReportProgress)
        {
            try
            {
                List<VendorDetails> ListTempCustDtls = new List<VendorDetails>();
                List<LineDetails> ListTempLineDtls = new List<LineDetails>();
                List<PriceGroupDetails> ListTempPGDtls = new List<PriceGroupDetails>();
                List<DiscountGroupDetails> ListTempDGDtls = new List<DiscountGroupDetails>();

                MySQLHelper ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                int LastVendorIDFromDB = ObjMySQLHelper.GetLatestColValFromTable("VendorID", "VendorMASTER");
                List<string> ListExistingVendorNamesinDB = CommonFunctions.ObjVendorMaster.GetVendorList();

                ImportFromExcelVendorCache ObjImportFromExcelVendorCache = new ImportFromExcelVendorCache();
                StreamReader srReadSelectedFile = new StreamReader(FilePathToImport);
                string HeaderStr = srReadSelectedFile.ReadLine();
                char ColSeparator = CommonFunctions.GetColSeparator(HeaderStr);
                string[] Header = HeaderStr.Split(ColSeparator);

                int VendorNameIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelVendorCache.VendorColName, StringComparison.InvariantCultureIgnoreCase));

                int StateIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelVendorCache.StateColName, StringComparison.InvariantCultureIgnoreCase));

                int ActiveIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelVendorCache.ActiveColName, StringComparison.InvariantCultureIgnoreCase));
                int AddressIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelVendorCache.AddressColName, StringComparison.InvariantCultureIgnoreCase));

                int GSTINIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelVendorCache.GSTINColName, StringComparison.InvariantCultureIgnoreCase));
                int AddedDateIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelVendorCache.AddedDateColName, StringComparison.InvariantCultureIgnoreCase));
                int LastUpdateDateIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelVendorCache.LastUpdateDateColName, StringComparison.InvariantCultureIgnoreCase));

                int PhoneIndex = Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelVendorCache.PhoneNoColName, StringComparison.InvariantCultureIgnoreCase));
                List<string> ListOfVendorAlreadyInDB = new List<string>();
                int VendorID = LastVendorIDFromDB + 1;

                if (VendorID == 0) VendorID += 1;
                while (srReadSelectedFile.Peek() != -1)
                {
                    string[] arr = srReadSelectedFile.ReadLine().Split(ColSeparator);
                    VendorDetails ObjVD = CommonFunctions.ObjVendorMaster.GetVendorDetails(arr[VendorNameIndex].Trim());
                    if (ObjVD != null)
                    {
                        ListOfVendorAlreadyInDB.Add(arr[VendorNameIndex]);
                        continue;
                    }
                    VendorDetails ObjVendorDetails = new VendorDetails();
                    ObjVendorDetails.VendorName = arr[VendorNameIndex].Trim();
                    ObjVendorDetails.Address = arr[AddressIndex].Trim();
                    ObjVendorDetails.GSTIN = arr[GSTINIndex].Trim();
                    ObjVendorDetails.PhoneNo = (arr[PhoneIndex] == null) ? "" : arr[PhoneIndex].Trim();
                    if (arr[ActiveIndex].Trim() != "") ObjVendorDetails.Active = bool.Parse(arr[ActiveIndex].Trim());
                    ObjVendorDetails.State = ((arr[StateIndex] == null) || arr[StateIndex].Trim() == "") ? "" : arr[StateIndex].Trim();

                    int Index = -1, AlreadyExistsIndex = -1;

                    AlreadyExistsIndex = ListExistingVendorNamesinDB.FindIndex(e => e.Equals(ObjVendorDetails.VendorName, StringComparison.InvariantCultureIgnoreCase));
                    if (AlreadyExistsIndex < 0)
                    {
                        Index = ListTempCustDtls.BinarySearch(ObjVendorDetails, ObjVendorDetails);
                        if (Index < 0)
                        {
                            ObjVendorDetails.VendorID = VendorID;
                            ObjVendorDetails.StateID = CommonFunctions.ObjCustomerMasterModel.GetStateID(ObjVendorDetails.State);
                            ListTempCustDtls.Insert(~Index, ObjVendorDetails);
                            VendorID++;
                        }
                    }
                }
                srReadSelectedFile.Close();

                if (ListTempCustDtls.Count > 0)
                {
                    CommonFunctions.ObjVendorMaster.FillVendorDBFromCache(ListTempCustDtls);

                    CommonFunctions.ObjVendorMaster.LoadNGetVendorDataTable();
                }

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ReadNProcessVendorFile()", ex);
                return -1;
            }
        }

        private void ManageVendorForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ManageVendorForm_Shown()", ex);
            }
        }

        private void btnSearchVendor_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> ListFindInFields = new List<String>()
                {
                    "Vendor Name",
                    "Vendor Phone",
                    "Vendor State"
                };
                CommonFunctions.ShowDialog(new OrderInvQuotSearchForm(ListFindInFields, PerformSearch, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchVendor_Click()", ex);
            }
        }

        private void PerformSearch(SearchDetails ObjSearchDtls)
        {
            try
            {
                AssignFilterDataTableToGrid(ObjSearchDtls);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AssignFilterDataTableToGrid(SearchDetails ObjSearchDtls)
        {
            try
            {
                string ModifiedStr = CommonFunctions.GetModifiedStringBasedOnMatchPatterns(ObjSearchDtls.SearchString, ObjSearchDtls.MatchPattern);
                var rows = dtAllVendors.Select(CommonFunctions.DictFilterNamesWithActualDBColNames[ObjSearchDtls.SearchIn] + " like '" + ModifiedStr + "'");
                dgvVendorCache.DataSource = null;
                if (dgvVendorCache.Columns.Count > 0) dgvVendorCache.Columns.Clear();
                if (rows.Any())
                {
                    LoadGridView(rows.CopyToDataTable());
                }
                else
                {
                    dgvVendorCache.DataSource = new BindingSource { DataSource = CommonFunctions.GetDataTableWhenNoRecordsFound() };
                    dgvVendorCache.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AssignFilterDataTableToGrid()", ex);
            }
        }
    }
}
