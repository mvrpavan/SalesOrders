using SalesOrdersReport.CommonModules;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public enum ImportDataTypes
    {
        Products, Customers , Payments
    };

    public delegate Int32 ImportDataFromFileDel(String FilePath, Object ObjDetails, ReportProgressDel ReportProgress);

    public partial class ImportFromExcelForm : Form
    {
        UpdateOnCloseDel UpdateOnClose = null;
        ImportDataTypes ImportDataType;
        ImportDataFromFileDel ImportDataFromFile = null;

        public ImportFromExcelForm(ImportDataTypes ImportDataType, UpdateOnCloseDel UpdateOnClose, ImportDataFromFileDel ImportDataFromFile)
        {
            try
            {
                InitializeComponent();
                this.UpdateOnClose = UpdateOnClose;
                this.ImportDataType = ImportDataType;
                this.ImportDataFromFile = ImportDataFromFile;

                btnImportFrmExclUploadFile.Enabled = false;
                txtImportFrmExclFilePath.ReadOnly = true;
                OFDImportExcelFileDialog.CheckFileExists = true;
                OFDImportExcelFileDialog.CheckPathExists = true;
                OFDImportExcelFileDialog.Multiselect = false;

                switch (this.ImportDataType)
                {
                    case ImportDataTypes.Products:
                        this.Text = "Import Products from Excel";
                        chkListBoxDataToImport.Items.Add("Product Details", true);
                        chkListBoxDataToImport.Items.Add("Category Details", false);
                        chkListBoxDataToImport.Items.Add("Product Inventory", false);
                        chkListBoxDataToImport.Items.Add("HSN Code (Tax Details)", false);
                        chkListBoxDataToImport.Items.Add("Vendor Details", false);
                        btnImportFrmExclUploadFile.Text = "Import Products";
                        OFDImportExcelFileDialog.Filter = "Excel Files(*.xlsx;*.xls)|*.xlsx;*.xls|All Files(*.*)|*.*";
                        OFDImportExcelFileDialog.Title = "Choose File to Import Products data from";
                        break;
                    case ImportDataTypes.Customers:
                        this.Text = "Import Customers from Excel";
                        btnImportFrmExclUploadFile.Text = "Import Customers";
                        chkListBoxDataToImport.Items.Add("Customer Details", true);
                        chkListBoxDataToImport.Enabled = false;
                        OFDImportExcelFileDialog.Filter = "Comma separated Files (*.csv)|*.csv|Text Files(*.txt)|*.txt";
                        OFDImportExcelFileDialog.Title = "Choose File to Import Customers data from";
                        break;
                    case ImportDataTypes.Payments:
                        this.Text = "Import Payments from Excel";
                        btnImportFrmExclUploadFile.Text = "Import Payments";
                        chkListBoxDataToImport.Items.Add("Payments Details", true);
                        chkListBoxDataToImport.Enabled = false;
                        OFDImportExcelFileDialog.Filter = "Excel Files(*.xlsx;*.xls)|*.xlsx;*.xls|All Files(*.*)|*.*";
                        OFDImportExcelFileDialog.Title = "Choose File to Import Payments data from";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ImportFromExcelForm.ctor()", ex);
            }
        }

        private void btnImportFrmExcelBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = OFDImportExcelFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtImportFrmExclFilePath.Text = OFDImportExcelFileDialog.FileName;
                    btnImportFrmExclUploadFile.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnImportFrmExcelBrowse_Click()", ex);
            }
        }

        private void btnImportFrmExclUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlgResult = new DialogResult();
                if (txtImportFrmExclFilePath.Text == string.Empty)
                {
                    dlgResult = OFDImportExcelFileDialog.ShowDialog();
                    if (dlgResult == DialogResult.OK) txtImportFrmExclFilePath.Text = OFDImportExcelFileDialog.FileName;
                }
                if (!File.Exists(txtImportFrmExclFilePath.Text))
                {
                    MessageBox.Show("Please Select a valid file path!!!", "Error");
                    return;
                }

                if (txtImportFrmExclFilePath.Text != string.Empty)
                {

                    if (chkListBoxDataToImport.CheckedItems.Count == 0)
                    {
                        MessageBox.Show(this, "Choose atleast one dataset to Import.", "Import data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }
#if DEBUG
                    bgWorkerImportExcel_DoWork(null, null);
                    bgWorkerImportExcel_RunWorkerCompleted(null, null);
#else
                    ReportProgress = bgWorkerImportExcel.ReportProgress;
                    bgWorkerImportExcel.WorkerReportsProgress = true;
                    bgWorkerImportExcel.RunWorkerAsync();
#endif
                    //txtImportFrmExclFilePath.Text = "";
                    //btnImportFrmExclUploadFile.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnImportFrmExclUploadFile_Click()", ex);
            }
        }

        ReportProgressDel ReportProgress = null;
        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        Int32 ImportResult = 0;
        private void bgWorkerImportExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
                Int32 Retval = -1;

                switch (ImportDataType)
                {
                    case ImportDataTypes.Products:
                        Boolean[] ArrOptionsSelected = new Boolean[chkListBoxDataToImport.Items.Count];
                        for (int i = 0; i < chkListBoxDataToImport.Items.Count; i++)
                        {
                            ArrOptionsSelected[i] = chkListBoxDataToImport.GetItemChecked(i);
                        }

                        Retval = ImportDataFromFile(txtImportFrmExclFilePath.Text, ArrOptionsSelected, ReportProgress);
                        if (Retval == 0)
                        {
                            MessageBox.Show(this, "Importing Products data from File to Database is successful!!!", "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ImportResult = 0;
                        }
                        else if (Retval == 1)
                        {
                            MessageBox.Show(this, "No new Products data available in File to import!!!", "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            ImportResult = 1;
                        }
                        else
                        {
                            MessageBox.Show(this, "Importing Products data from File to Database failed!!!", "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            ImportResult = -1;
                        }
                        break;
                    case ImportDataTypes.Customers:
                        Retval = ImportDataFromFile(txtImportFrmExclFilePath.Text, null, ReportProgress);
                        if (Retval == 0)
                        {
                            MessageBox.Show(this, "Importing Customers data from File to Database is successful!!!", "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ImportResult = 0;
                        }
                        else
                        {
                            MessageBox.Show(this, "Importing Customers data from File to Database failed!!!", "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            ImportResult = -1;
                        }
                        //MessageBox.Show("Importing Customer File to DB Successful!!!");
                        break;
                    case ImportDataTypes.Payments:
                        Retval = ImportDataFromFile(txtImportFrmExclFilePath.Text, null, ReportProgress);
                        if (Retval == 0)
                        {
                            MessageBox.Show(this, "Importing Customers data from File to Database is successful!!!", "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ImportResult = 0;
                        }
                        else
                        {
                            MessageBox.Show(this, "Importing Customers data from File to Database failed!!!", "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            ImportResult = -1;
                        }
                        //MessageBox.Show("Importing Customer File to DB Successful!!!");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.bgWorkerImportExcel_DoWork()", ex);
            }
        }

        private void bgWorkerImportExcel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
        }

        private void bgWorkerImportExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
                if (ImportResult != 0) return;

                switch (ImportDataType)
                {
                    case ImportDataTypes.Products:
                        UpdateOnClose(Mode: 1);
                        break;
                    case ImportDataTypes.Customers:
                        UpdateOnClose(Mode: 1);
                        break;
                    default:
                        break;
                }

                CommonFunctions.ResetProgressBar();
                txtImportFrmExclFilePath.Text = "";
                btnImportFrmExclUploadFile.Enabled = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.bgWorkerImportExcel_RunWorkerCompleted()", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
