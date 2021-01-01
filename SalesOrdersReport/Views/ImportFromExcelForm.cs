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
    public enum IMPORTDATATYPES
    {
        PRODUCTS,
        CUSTOMERS
    };

    public partial class ImportFromExcelForm : Form
    {
        UpdateOnCloseDel UpdateOnClose = null;
        IMPORTDATATYPES ImportDataType;

        public ImportFromExcelForm(IMPORTDATATYPES ImportDataType, UpdateOnCloseDel UpdateOnClose)
        {
            try
            {
                InitializeComponent();
                this.UpdateOnClose = UpdateOnClose;
                this.FormClosed += ImportFromExcelForm_FormClosed;
                this.ImportDataType = ImportDataType;

                switch (this.ImportDataType)
                {
                    case IMPORTDATATYPES.PRODUCTS:
                        this.Text = "Import Products from Excel";
                        chkListBoxDataToImport.Items.Add("Product Details", true);
                        chkListBoxDataToImport.Items.Add("Category Details", false);
                        chkListBoxDataToImport.Items.Add("Product Inventory", false);
                        chkListBoxDataToImport.Items.Add("HSN Code (Tax Details)", false);
                        break;
                    case IMPORTDATATYPES.CUSTOMERS:
                        this.Text = "Import Customers from Excel";
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

        private void ImportFromExcelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdateOnClose(Mode: 2);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ImportFromExcelForm.ImportFromExcelForm_FormClosed()", ex);
            }
        }
    }
}
