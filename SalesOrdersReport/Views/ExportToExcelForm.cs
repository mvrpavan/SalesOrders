using SalesOrdersReport.CommonModules;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{

    public enum ExportDataTypes
    {
        Products, Customers, Orders
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
                        btnExportToExcelFile.Text = "Export Products Data to Excel File";
                        saveFileDialogExportToExcel.Title = "Save Exported file as";
                        saveFileDialogExportToExcel.OverwritePrompt = false;
                        chkBoxAppend.Checked = false;
                        break;
                    case ExportDataTypes.Orders:
                        this.Text = "Export Orders Data to Excel";
                        chkListBoxDataToExport.Items.Add("Export all displayed Orders", true);
                        chkListBoxDataToExport.Items.Add("Export only selected Order", false);
                        btnExportToExcelFile.Text = "Export Orders Data to Excel File";
                        folderBrowserDialogExport.Description = "Save Exported file to";
                        chkBoxAppend.Checked = false;
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
                        result = folderBrowserDialogExport.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            txtExportToExcelFilePath.Text = folderBrowserDialogExport.SelectedPath;
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

                            if (chkListBoxDataToExport.CheckedItems.Count == 0)
                            {
                                MessageBox.Show(this, "Choose atleast one dataset to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return;
                            }

                            if (!chkBoxAppend.Checked && File.Exists(FilePath))
                            {
                                DialogResult dialogResult = MessageBox.Show(this, "Do you want to overwrite the file?", "Export data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                if (dialogResult == DialogResult.No) return;
                            }
                            break;
                        case ExportDataTypes.Orders:
                            if (chkListBoxDataToExport.CheckedItems.Count == 0)
                            {
                                MessageBox.Show(this, "Choose an option to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return;
                            }

                            if (chkListBoxDataToExport.CheckedItems.Count == 2)
                            {
                                MessageBox.Show(this, "Choose only one option to Export.", "Export data", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                return;
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
                    txtExportToExcelFilePath.Text = "";
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
                        Int32 OptionsSelected = -1;
                        for (int i = 0; i < chkListBoxDataToExport.Items.Count; i++)
                        {
                            if (chkListBoxDataToExport.GetItemChecked(i))
                            {
                                OptionsSelected = i;
                                break;
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
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
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
