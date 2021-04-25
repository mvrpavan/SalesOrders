using SalesOrdersReport.CommonModules;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{

    public enum ExportDataTypes
    {
        Products, Customers, Orders, Invoices, Payments, Stocks ,Reports
    };

    public delegate Int32 ExportDataToFileDel(String FilePath, Object ObjDetails, Boolean Append);

    public partial class ExportToExcelForm : Form
    {
        UpdateOnCloseDel UpdateOnClose = null;
        ExportDataTypes ExportDataType;
        ExportDataToFileDel ExportDataToFile = null;

        public ExportToExcelForm(ExportDataTypes ExportDataType, UpdateOnCloseDel UpdateOnClose, ExportDataToFileDel ExportDataToFile)
        {
            try
            {
                InitializeComponent();
                this.UpdateOnClose = UpdateOnClose;
                this.ExportDataType = ExportDataType;
                this.ExportDataToFile = ExportDataToFile;

                btnExportToExcelFile.Enabled = true;
                txtExportToExcelFilePath.ReadOnly = false;
                saveFileDialogExportToExcel.AddExtension = true;
                saveFileDialogExportToExcel.Filter = "Excel File(*.xlsx)|*.xlsx";
                saveFileDialogExportToExcel.ValidateNames = true;
                saveFileDialogExportToExcel.RestoreDirectory = true;

                switch (this.ExportDataType)
                {
                    case ExportDataTypes.Products:
                        this.Text = "Export Products Data to Excel";
                        chkListBoxDataToExport.Items.Add("Product Details", true);
                        //chkListBoxDataToExport.Items.Add("Category Details", false);
                        chkListBoxDataToExport.Items.Add("Product Inventory", false);
                        //chkListBoxDataToExport.Items.Add("HSN Code (Tax Details)", false);
                        lblExport.Text = "Export to File";
                        btnExportToExcelFile.Text = "Export Products Data to Excel File";
                        saveFileDialogExportToExcel.Title = "Save Exported file as";
                        saveFileDialogExportToExcel.OverwritePrompt = false;
                        chkBoxAppend.Checked = false;
                        break;
                    case ExportDataTypes.Orders:
                        this.Text = "Export Orders Data to Excel";
                        chkListBoxDataToExport.Items.Add("Export all displayed Orders", true);
                        chkListBoxDataToExport.Items.Add("Export only selected Order", false);
                        chkListBoxDataToExport.Items.Add("Export Item Summary", false);
                        lblExport.Text = "Export to Folder";
                        btnExportToExcelFile.Text = "Export Orders Data to Excel File";
                        folderBrowserDialogExport.Description = "Save Exported file to";
                        chkBoxAppend.Visible = false;
                        break;
                    case ExportDataTypes.Invoices:
                        this.Text = "Export Invoices Data to Excel";
                        chkListBoxDataToExport.Items.Add("Export all displayed Invoices", true);
                        chkListBoxDataToExport.Items.Add("Export only selected Invoice", false);
                        chkListBoxDataToExport.Items.Add("Export Seller Summary", false);
                        lblExport.Text = "Export to Folder";
                        btnExportToExcelFile.Text = "Export Invoices Data to Excel File";
                        folderBrowserDialogExport.Description = "Save Exported file to";
                        chkBoxAppend.Visible = false;
                        break;
                    case ExportDataTypes.Payments:
                        this.Text = "Export Payments/Summary Data to Excel";
                        chkListBoxDataToExport.Items.Add("Payment/Summary Details", true);
                        lblExport.Text = "Export to File";
                        btnExportToExcelFile.Text = "Export Payments/Summary Data to Excel File";
                        saveFileDialogExportToExcel.Title = "Save Exported file as";
                        chkBoxAppend.Visible = false;
                        break;
                    case ExportDataTypes.Stocks:
                        this.Text = "Export Stocks Data to Excel";
                        chkListBoxDataToExport.Items.Add("Stocks Details", true);
                        lblExport.Text = "Export to File";
                        btnExportToExcelFile.Text = "Export Stocks Data to Excel File";
                        saveFileDialogExportToExcel.Title = "Save Exported file as";
                        chkBoxAppend.Visible = false;
                        break;
                    case ExportDataTypes.Reports:
                        this.Text = "Export Reports Data to Excel";
                        chkListBoxDataToExport.Items.Add("Reports Details", true);
                        lblExport.Text = "Export to File";
                        btnExportToExcelFile.Text = "Export Reports Data to Excel File";
                        saveFileDialogExportToExcel.Title = "Save Exported file as";
                        chkBoxAppend.Visible = false;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ExportToExcelForm.ctor()", ex);
            }
        }

        private void btnExportToExcelBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result;
                switch (this.ExportDataType)
                {
                    case ExportDataTypes.Products:
                        result = saveFileDialogExportToExcel.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            txtExportToExcelFilePath.Text = saveFileDialogExportToExcel.FileName;
                        }
                        break;
                    case ExportDataTypes.Orders:
                    case ExportDataTypes.Invoices:
                        result = folderBrowserDialogExport.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            txtExportToExcelFilePath.Text = folderBrowserDialogExport.SelectedPath;
                        }
                        break;
                    case ExportDataTypes.Payments:
                        result = saveFileDialogExportToExcel.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            txtExportToExcelFilePath.Text = saveFileDialogExportToExcel.FileName;
                        }
                        break;
                    case ExportDataTypes.Stocks:
                        result = saveFileDialogExportToExcel.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            txtExportToExcelFilePath.Text = saveFileDialogExportToExcel.FileName;
                        }
                        break;
                    case ExportDataTypes.Reports:
                        result = saveFileDialogExportToExcel.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            txtExportToExcelFilePath.Text = saveFileDialogExportToExcel.FileName;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportToExcelBrowse_Click()", ex);
            }
        }

        private void btnExportToExcelFile_Click(object sender, EventArgs e)
        {
            try
            {
                switch (ExportDataType)
                {
                    case ExportDataTypes.Products:
                        if (chkListBoxDataToExport.CheckedItems.Count == 0)
                        {
                            MessageBox.Show(this, "Choose atleast one dataset to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        break;
                    case ExportDataTypes.Orders:
                        if (chkListBoxDataToExport.CheckedItems.Count == 0)
                        {
                            MessageBox.Show(this, "Choose an option to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        if (chkListBoxDataToExport.CheckedItems.Count == 1 && chkListBoxDataToExport.CheckedItems.Contains(2))
                        {
                            MessageBox.Show(this, "Choose another option along with Export Item Summary.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        if (chkListBoxDataToExport.CheckedIndices.Contains(0) && chkListBoxDataToExport.CheckedIndices.Contains(1))
                        {
                            MessageBox.Show(this, "Choose either All orders option or Selected order option to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        break;
                    case ExportDataTypes.Invoices:
                        if (chkListBoxDataToExport.CheckedItems.Count == 0)
                        {
                            MessageBox.Show(this, "Choose an option to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        if (chkListBoxDataToExport.CheckedItems.Count == 1 && chkListBoxDataToExport.CheckedItems.Contains(2))
                        {
                            MessageBox.Show(this, "Choose another option along with Export Item Summary.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        if (chkListBoxDataToExport.CheckedIndices.Contains(0) && chkListBoxDataToExport.CheckedIndices.Contains(1))
                        {
                            MessageBox.Show(this, "Choose either All Invoices option or Selected Invoice option to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        break;
                    case ExportDataTypes.Payments:
                        if (chkListBoxDataToExport.CheckedItems.Count == 0)
                        {
                            MessageBox.Show(this, "Choose atleast one dataset to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        break;
                    case ExportDataTypes.Stocks:
                    case ExportDataTypes.Reports:
                        if (chkListBoxDataToExport.CheckedItems.Count == 0)
                        {
                            MessageBox.Show(this, "Choose atleast one dataset to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        break;
                    default:
                        break;
                }

                if (txtExportToExcelFilePath.Text == string.Empty)
                    btnExportToExcelBrowse.PerformClick();

                if (txtExportToExcelFilePath.Text != string.Empty)
                {
                    String FilePath = txtExportToExcelFilePath.Text;

                    switch (ExportDataType)
                    {
                        case ExportDataTypes.Products:
                            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                            {
                                MessageBox.Show(this, "Choose a valid directory to Save file to.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return;
                            }

                            if (!chkBoxAppend.Checked && File.Exists(FilePath))
                            {
                                DialogResult dialogResult = MessageBox.Show(this, "Do you want to overwrite the file?", "Export data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                if (dialogResult == DialogResult.No) return;
                            }
                            break;
                        case ExportDataTypes.Orders:
                            break;
                        case ExportDataTypes.Payments:
                            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                            {
                                MessageBox.Show(this, "Choose a valid directory to Save file to.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return;
                            }

                            if (!chkBoxAppend.Checked && File.Exists(FilePath))
                            {
                                DialogResult dialogResult = MessageBox.Show(this, "Do you want to overwrite the file?", "Export data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                if (dialogResult == DialogResult.No) return;
                            }
                            break;
                        case ExportDataTypes.Stocks:
                        case ExportDataTypes.Reports:
                            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                            {
                                MessageBox.Show(this, "Choose a valid directory to Save file to.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return;
                            }

                            if (!chkBoxAppend.Checked && File.Exists(FilePath))
                            {
                                DialogResult dialogResult = MessageBox.Show(this, "Do you want to overwrite the file?", "Export data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                if (dialogResult == DialogResult.No) return;
                            }
                            break;
                        default:
                            break;
                    }
#if DEBUG
                    bgWorkerExportExcel_DoWork(null, null);
                    bgWorkerExportExcel_RunWorkerCompleted(null, null);
#else
                    bgWorkerExportExcel.WorkerReportsProgress = true;
                    bgWorkerExportExcel.RunWorkerAsync();
#endif
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportToExcelFile_Click()", ex);
            }
        }

        Int32 ExportResult = 0;
        private void bgWorkerExportExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
                Int32 Retval = -1;

                switch (ExportDataType)
                {
                    case ExportDataTypes.Products:
                        Boolean[] ArrOptionsSelected = new Boolean[chkListBoxDataToExport.Items.Count];
                        for (int i = 0; i < chkListBoxDataToExport.Items.Count; i++)
                        {
                            ArrOptionsSelected[i] = chkListBoxDataToExport.GetItemChecked(i);
                        }

                        Retval = ExportDataToFile(txtExportToExcelFilePath.Text, ArrOptionsSelected, chkBoxAppend.Checked);

                        if (Retval == 0)
                        {
                            MessageBox.Show(this, "Exporting Products data to Excel File is successful!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ExportResult = 0;
                        }
                        else if (Retval == 1)
                        {
                            MessageBox.Show(this, "No Products data available to export!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            ExportResult = 1;
                        }
                        else
                        {
                            MessageBox.Show(this, "Exporting Products data to Excel file failed!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            ExportResult = -1;
                        }
                        break;
                    case ExportDataTypes.Orders:
                        {
                            Int32 OptionsSelected = 0;
                            for (int i = 0; i < chkListBoxDataToExport.Items.Count; i++)
                            {
                                if (chkListBoxDataToExport.GetItemChecked(i))
                                {
                                    OptionsSelected += (Int32)Math.Pow(2, i);
                                }
                            }

                            Retval = ExportDataToFile(txtExportToExcelFilePath.Text, OptionsSelected, chkBoxAppend.Checked);
                            if (Retval == 0)
                            {
                                MessageBox.Show(this, "Exporting Orders data to Excel File is successful!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                ExportResult = 0;
                            }
                            else if (Retval == 1)
                            {
                                MessageBox.Show(this, "No Orders data available to export!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                ExportResult = 1;
                            }
                            else
                            {
                                MessageBox.Show(this, "Exporting Orders data to Excel file failed!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                ExportResult = -1;
                            }
                        }
                        break;
                    case ExportDataTypes.Invoices:
                        {
                            Int32 OptionsSelected = 0;
                            for (int i = 0; i < chkListBoxDataToExport.Items.Count; i++)
                            {
                                if (chkListBoxDataToExport.GetItemChecked(i))
                                {
                                    OptionsSelected += (Int32)Math.Pow(2, i);
                                }
                            }

                            Retval = ExportDataToFile(txtExportToExcelFilePath.Text, OptionsSelected, chkBoxAppend.Checked);
                            if (Retval == 0)
                            {
                                MessageBox.Show(this, "Exporting Invoices data to Excel File is successful!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                ExportResult = 0;
                            }
                            else if (Retval == 1)
                            {
                                MessageBox.Show(this, "No Invoices data available to export!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                ExportResult = 1;
                            }
                            else if (Retval == 2)
                            {
                                ExportResult = 2;
                            }
                            else
                            {
                                MessageBox.Show(this, "Exporting Invoices data to Excel file failed!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                ExportResult = -1;
                            }
                        }
                        break;
                    case ExportDataTypes.Payments:

                        Retval = ExportDataToFile(txtExportToExcelFilePath.Text, null, chkBoxAppend.Checked);

                        if (Retval == 0)
                        {
                            MessageBox.Show(this, "Exporting Payments/Summary data to Excel File is successful!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ExportResult = 0;
                        }
                        else if (Retval == 1)
                        {
                            MessageBox.Show(this, "No Payments/Summary data available to export!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            ExportResult = 1;
                        }
                        else
                        {
                            MessageBox.Show(this, "Exporting Payments/Summary data to Excel file failed!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            ExportResult = -1;
                        }
                        break;
                    case ExportDataTypes.Stocks:
                    case ExportDataTypes.Reports:
                        Retval = ExportDataToFile(txtExportToExcelFilePath.Text, null, chkBoxAppend.Checked);
                        string strVal = (ExportDataType == ExportDataTypes.Stocks) ? "Stocks" : "Reports";
                        if (Retval == 0)
                        {
                            MessageBox.Show(this, "Exporting " + strVal + " data to Excel File is successful!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ExportResult = 0;
                        }
                        else if (Retval == 1)
                        {
                            MessageBox.Show(this, "No " + strVal + " data available to export!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            ExportResult = 1;
                        }
                        else
                        {
                            MessageBox.Show(this, "Exporting " + strVal + " data to Excel file failed!!!", "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            ExportResult = -1;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.bgWorkerExportExcel_DoWork()", ex);
            }
        }

        private void bgWorkerExportExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                txtExportToExcelFilePath.Text = "";
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
                CommonFunctions.ResetProgressBar();
                if (ExportResult != 0) return;

                switch (ExportDataType)
                {
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.bgWorkerExportExcel_RunWorkerCompleted()", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
