using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class ImportFromExcelForm : Form
    {
        UpdateCustomerOnCloseDel UpdateCustomerOnClose = null;
        public ImportFromExcelForm(UpdateCustomerOnCloseDel UpdateCustomerOnClose)
        {
            InitializeComponent();
            //txtNewLineName.Focus();
            this.UpdateCustomerOnClose = UpdateCustomerOnClose;
            this.FormClosed += ImportFromExcelForm_FormClosed;
        }
        private void btnImportFrmExcelBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = OFDImportExcelFileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    txtImportFrmExclFilePath.Text = OFDImportExcelFileDialog.FileName;
                    //btnImportFrmExclUploadFile.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ImportFromExcelForm.btnImportFrmExcelBrowse_Click()", ex);
                throw ex;
            }
        }

        private void btnImportFrmExclUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                // int count = 0;
                //string[] FilenameName;
                //foreach (string item in OFDImportExcelFileDialog.FileNames)
                //{
                //FilenameName = item.Split('\\');
                //File.Copy(item, @"Images\" + FilenameName[FilenameName.Length - 1]);
                //count++;
                //}
                // OFDImportExcelFileDialog.Multiselect = false;
                //OFDImportExcelFileDialog.FileName = "";
                DialogResult dlgResult = new DialogResult();
                if (txtImportFrmExclFilePath.Text == string.Empty)
                {
                    dlgResult = OFDImportExcelFileDialog.ShowDialog();
                    txtImportFrmExclFilePath.Text = OFDImportExcelFileDialog.FileName;
                }
                if (!File.Exists(OFDImportExcelFileDialog.FileName))
                {
                    MessageBox.Show("Please Select a valid file path!!!", "Error");
                    return;
                }

                if (dlgResult == System.Windows.Forms.DialogResult.OK || txtImportFrmExclFilePath.Text != string.Empty)
                {
                    //
                  
#if DEBUG
                    ReadNProcessCustomerFile();
#else
              
                bgWorkerImportExcelCreatePO.WorkerReportsProgress = true;
                bgWorkerImportExcelCreatePO.RunWorkerAsync();
#endif

                    MessageBox.Show("Importing Customer File to DB Successful!!!");
                    txtImportFrmExclFilePath.Text = "";
                    //btnImportFrmExclUploadFile.Enabled = false;

                }

                //MessageBox.Show(Convert.ToString(count) + " File(s) copied");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ImportFromExcelForm.btnImportFrmExclUploadFile_Click()", ex);
            }
        }

        private void ReadNProcessCustomerFile()
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
                StreamReader srReadSelectedFile = new StreamReader(OFDImportExcelFileDialog.FileName);
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

                int PG_DiscountTypeIndex =  Array.FindIndex(Header, y => y.Equals(ObjImportFromExcelCustomerCache.PGDiscCountTypeColName, StringComparison.InvariantCultureIgnoreCase));
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
                    // ObjCustomerDetails.CustomerID = int.Parse(dtRow["CUSTOMERID"].ToString().Trim());
                    ObjCustomerDetails.CustomerName = arr[CustomerNameIndex].Trim();
                    ObjCustomerDetails.Address = arr[AddressIndex].Trim();
                    ObjCustomerDetails.GSTIN = arr[GSTINIndex].Trim();
                    ObjCustomerDetails.PhoneNo = ((arr[PhoneIndex] == null) || arr[PhoneIndex].Trim() == "") ? 0 : Int64.Parse(arr[PhoneIndex].Trim());
                    //ObjCustomerDetails.Active = bool.Parse(arr[ActiveIndex].ToString().Trim() == "1" ? "true" : "false");
                    if (arr[ActiveIndex].Trim() != "") ObjCustomerDetails.Active = bool.Parse(arr[ActiveIndex].Trim());
                    ObjCustomerDetails.State = ((arr[StateIndex] == null) || arr[StateIndex].Trim() == "") ? "": arr[StateIndex].Trim();
                    ObjCustomerDetails.OrderDaysAssigned = arr[OrderDaysIndex].Trim();
                    //ObjCustomerDetails.LastUpdateDate = ((arr[LastUpdateDateIndex] == null) || arr[LastUpdateDateIndex].ToString().Trim() == "") ? DateTime.MinValue : DateTime.Parse(arr[LastUpdateDateIndex].ToString());
                    //ObjCustomerDetails.AddedDate = ((arr[AddedDateIndex] == null) || arr[AddedDateIndex].ToString().Trim() == "") ? DateTime.MinValue : DateTime.Parse(arr[AddedDateIndex].ToString());
                    ObjCustomerDetails.LineName = ((arr[LineNameIndex] == null) || arr[LineNameIndex].Trim() == "") ? "" : arr[LineNameIndex].Trim();
                    ObjCustomerDetails.DiscountGroupName = ((arr[DiscountGroupNameIndex] == null) || arr[DiscountGroupNameIndex].Trim() == "") ? "" : arr[DiscountGroupNameIndex].Trim();
                    ObjCustomerDetails.PriceGroupName = ((arr[PriceGroupNameIndex] == null) || arr[PriceGroupNameIndex].Trim() == "") ? "" : arr[PriceGroupNameIndex].Trim();
                    int Index = -1, AlreadyExistsIndex = -1; int ResultVal = -1;
                    
                    
                    
                    AlreadyExistsIndex = ListExistingLineNamesinDB.FindIndex(e => e.Equals(ObjCustomerDetails.LineName, StringComparison.InvariantCultureIgnoreCase));
                    if (AlreadyExistsIndex < 0)
                    {
                        LineDetails ObjLineDetails = new LineDetails();
                        ObjLineDetails.LineName = ObjCustomerDetails.LineName;
                        Index = ListTempLineDtls.BinarySearch(ObjLineDetails, ObjLineDetails);
                        if (Index < 0)
                        {
                            //&&&&&
                            //ResultVal = CommonFunctions.ObjCustomerMasterModel.CreateNewLine(ObjLineDetails.LineName, "");
                            //if (ResultVal < 0) MessageBox.Show("Wasnt able to create line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //else if (ResultVal == 2) MessageBox.Show("Line already Exists, Please try adding new Line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //else
                            //{
                            ObjLineDetails.LineID = ObjCustomerDetails.LineID = LineID;
                            ListTempLineDtls.Insert(~Index, ObjLineDetails);
                            //CommonFunctions.ObjCustomerMasterModel.AddLineDetailsToCache(ObjLineDetails);
                            LineID++;
                            // }
                        }
                        else ListTempLineDtls[Index] = ObjLineDetails;
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
                            //ResultVal = CommonFunctions.ObjCustomerMasterModel.CreateNewDiscountGrp(ObjDiscountGroupDetails.DiscountGrpName,"");
                            //if (ResultVal < 0) MessageBox.Show("Wasnt able to create line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //else if (ResultVal == 2) MessageBox.Show("Line already Exists, Please try adding new Line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //else
                            //{
                            ObjDiscountGroupDetails.DiscountGrpID = ObjCustomerDetails.DiscountGroupID = DGID;
                            ObjDiscountGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(arr[DG_DiscountTypeIndex].Trim());
                            ObjDiscountGroupDetails.IsDefault = bool.Parse(arr[DGDefaultIndex].Trim());
                            if (arr[DGDefaultIndex].Trim() != "") ListTempDGDtls.Insert(~Index, ObjDiscountGroupDetails);
                            //CommonFunctions.ObjCustomerMasterModel.AddDiscountGroupToCache(ObjDiscountGroupDetails);
                            DGID++;
                            //}
                        }
                        else ListTempDGDtls[Index] = ObjDiscountGroupDetails;
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
                            //ResultVal = CommonFunctions.ObjCustomerMasterModel.CreateNewLine(ObjLineDetails.LineName, "");
                            //if (ResultVal < 0) MessageBox.Show("Wasnt able to create line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //else if (ResultVal == 2) MessageBox.Show("Line already Exists, Please try adding new Line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //else
                            //{
                            ObjPriceGroupDetails.PriceGroupID = ObjCustomerDetails.PriceGroupID = PGID;
                            ObjPriceGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(arr[PG_DiscountTypeIndex].Trim());
                            ObjPriceGroupDetails.IsDefault = bool.Parse(arr[PGDefaultIndex].Trim());
                            if (arr[PGDefaultIndex].Trim() != "") ObjPriceGroupDetails.PriceGrpCol = arr[SelectedPriceGroupColNameIndex].Trim();
                            ListTempPGDtls.Insert(~Index, ObjPriceGroupDetails);
                            //CommonFunctions.ObjCustomerMasterModel.AddPriceGroupToCache(ObjPriceGroupDetails);
                            PGID++;
                            //}
                        }
                        else ListTempPGDtls[Index] = ObjPriceGroupDetails;
                    }
                    else ObjCustomerDetails.PriceGroupID = CommonFunctions.ObjCustomerMasterModel.GetPriceGrpID(ListExistingPriceGrpNamesinDB[AlreadyExistsIndex]);
                    //ObjCustomerDetails.LineID = ((arr["LINEID"] == null) || arr["LINEID"].ToString().Trim() == "") ? -1 : int.Parse(arr["LINEID"].ToString().Trim());
                    //ObjCustomerDetails.DiscountGroupID = ((arr["DISCOUNTGROUPID"] == null) || arr["DISCOUNTGROUPID"].ToString().Trim() == "") ? -1 : int.Parse(arr["DISCOUNTGROUPID"].ToString().Trim());
                    //ObjCustomerDetails.PriceGroupID = ((arr["PRICEGROUPID"] == null) || arr["PRICEGROUPID"].ToString().Trim() == "") ? -1 : int.Parse(arr["PRICEGROUPID"].ToString().Trim());
                    //if (ObjCustomerDetails.LineID != -1) ObjCustomerDetails.LineName = ListLineDetails.Where(e => e.LineID.Equals(ObjCustomerDetails.LineID)).FirstOrDefault().LineName;
                    //if (ObjCustomerDetails.DiscountGroupID != -1) ObjCustomerDetails.DiscountGroupName = ListDiscountGroupDetails.Where(e => e.DiscountGrpID.Equals(ObjCustomerDetails.DiscountGroupID)).FirstOrDefault().DiscountGrpName;
                    //if (ObjCustomerDetails.PriceGroupID != -1) ObjCustomerDetails.PriceGroupName = ListPriceGroupDetails.Where(e => e.PriceGroupID.Equals(ObjCustomerDetails.PriceGroupID)).FirstOrDefault().PriceGrpName;
                    //if (ObjCustomerDetails.StateID != -1) ObjCustomerDetails.State = ListStateDetails.Where(e => e.StateID.Equals(ObjCustomerDetails.StateID)).FirstOrDefault().State;
                    AlreadyExistsIndex = ListExistingCustomerNamesinDB.FindIndex(e => e.Equals(ObjCustomerDetails.CustomerName, StringComparison.InvariantCultureIgnoreCase));
                    if (AlreadyExistsIndex < 0)
                    {
                        Index = ListTempCustDtls.BinarySearch(ObjCustomerDetails, ObjCustomerDetails);
                        if (Index < 0)
                        {
                            //ResultVal = CommonFunctions.ObjCustomerMasterModel.CreateNewLine(ObjLineDetails.LineName, "");
                            //if (ResultVal < 0) MessageBox.Show("Wasnt able to create line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //else if (ResultVal == 2) MessageBox.Show("Line already Exists, Please try adding new Line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //else
                            //{
                            ObjCustomerDetails.CustomerID = CustID;
                            ObjCustomerDetails.StateID = CommonFunctions.ObjCustomerMasterModel.GetStateID(ObjCustomerDetails.State);
                            ListTempCustDtls.Insert(~Index, ObjCustomerDetails);
                           // CommonFunctions.ObjCustomerMasterModel.AddCustomerDataToCache(ObjCustomerDetails);
                            CustID++;
                            //}
                        }
                        else ListTempCustDtls[Index] = ObjCustomerDetails;
                    }
                }
                srReadSelectedFile.Close();

                if (ListTempLineDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.FillLineDBFromCache(ListTempLineDtls);
                if (ListTempDGDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.FillDiscountGroupDBFromCache(ListTempDGDtls);
                if (ListTempPGDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.FillPriceGroupDBFromCache(ListTempPGDtls);
                if (ListTempCustDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.FillCustomerDBFromCache(ListTempCustDtls);

                if (ListTempLineDtls.Count > 0 || ListTempDGDtls.Count > 0 || ListTempPGDtls.Count > 0 || ListTempCustDtls.Count > 0) CommonFunctions.ObjCustomerMasterModel.LoadAllCustomerMasterTables();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ImportFromExcelForm.ReadNProcessCustomerFile()", ex);
                throw ex;
            }
        }

        private void ImportFromExcelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdateCustomerOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ImportFromExcelForm.ImportFromExcelForm_FormClosed()", ex);
            }
        }

        private void bgWorkerImportExcelCreatePO_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
                ReadNProcessCustomerFile();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ImportFromExcelForm.bgWorkerImportExcelCreatePO_DoWork()", ex);
            }
        }

        private void bgWorkerImportExcelCreatePO_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
               
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ImportFromExcelForm.bgWorkerImportExcelCreatePO_RunWorkerCompleted()", ex);
            }
        }
    }
}
