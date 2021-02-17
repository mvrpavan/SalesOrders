using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Views
{
    public delegate void PerformSearchDel(SearchDetails ObjSearchDetails);

    public partial class OrderInvQuotSearchForm : Form
    {
        UpdateOnCloseDel UpdateOnClose;
        PerformSearchDel PerformSearch;
        List<String> ListFindInFields = new List<String>();
        String[] ArrMatchPatterns = new String[] { "Starts With", "Ends With", "Contains", "Whole" };

        public OrderInvQuotSearchForm(List<String> ListFindInFields, PerformSearchDel PerformSearch, UpdateOnCloseDel UpdateOnClose, SearchDetails ObjSearchDetails = null)
        {
            try
            {
                InitializeComponent();
                this.UpdateOnClose = UpdateOnClose;
                this.PerformSearch = PerformSearch;

                cmbBoxSearchIn.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxMatch.DropDownStyle = ComboBoxStyle.DropDownList;
                this.ListFindInFields.AddRange(ListFindInFields);

                cmbBoxSearchIn.Items.Clear();
                cmbBoxSearchIn.Items.AddRange(this.ListFindInFields.ToArray());
                cmbBoxSearchIn.SelectedIndex = 0;

                cmbBoxMatch.Items.Clear();
                cmbBoxMatch.Items.AddRange(ArrMatchPatterns);
                cmbBoxMatch.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.OrderInvQuotSearchForm()", ex);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO
                MessageBox.Show(this, "Feature not implemented", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnFind_Click()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnClose_Click()", ex);
            }
        }
    }

    public enum MatchPatterns
    {
        StartsWith = 0, EndsWith, Contains, Whole
    }

    public class SearchDetails
    {
        public String SearchString, SearchIn;
        public MatchPatterns MatchPattern;
        public Boolean MatchCase;
    }
}
