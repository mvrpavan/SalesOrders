using System;
using System.Collections.Generic;
using System.Data;
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
        String[] ArrMatchPatterns = new String[] { "Starts With", "Ends With", "Contains", "Equals" };
        //SearchPatternModel ObjSearchPatternModel;
        public DataTable DtSearchResult;
        public OrderInvQuotSearchForm(List<String> ListFindInFields, PerformSearchDel PerformSearch, UpdateOnCloseDel UpdateOnClose, SearchDetails ObjSearchDetails = null)
        {
            try
            {
                InitializeComponent();
                this.UpdateOnClose = UpdateOnClose;
                this.PerformSearch = PerformSearch;
               // ObjSearchPatternModel = new SearchPatternModel();

                cmbBoxSearchIn.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxMatch.DropDownStyle = ComboBoxStyle.DropDownList;
                this.ListFindInFields.AddRange(ListFindInFields);

                cmbBoxSearchIn.Items.Clear();
                cmbBoxSearchIn.Items.AddRange(this.ListFindInFields.ToArray());
                cmbBoxSearchIn.SelectedIndex = 0;

                cmbBoxMatch.Items.Clear();
                cmbBoxMatch.Items.AddRange(ArrMatchPatterns);
                cmbBoxMatch.SelectedIndex = 3;

                //if (ObjSearchDetails != null)
                //{
                //    cmbBoxSearchIn.SelectedItem = ObjSearchDetails.SearchIn;
                //    cmbBoxMatch.SelectedIndex = Array.FindIndex(ArrMatchPatterns, e => e.Replace(" ", "").Equals(ObjSearchDetails.MatchPattern.ToString()));
                //    txtBoxSearchString.Text = ObjSearchDetails.SearchString;
                //    chkMatchCase.Checked = ObjSearchDetails.MatchCase;
                //}
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
                //MessageBox.Show(this, "Feature not implemented", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // txtBoxSearchString.Text
                if (txtBoxSearchString.Text.Trim() == string.Empty)
                {
                    return;
                }
                PerformSearch(new SearchDetails() { SearchString = txtBoxSearchString.Text.Trim() ,
                    SearchIn = cmbBoxSearchIn.SelectedItem.ToString(),
                    MatchPattern = GetMatchPattern(cmbBoxMatch.SelectedItem.ToString()),
                    MatchCase = chkMatchCase.Checked }
                );
             
                //MatchPatterns SelMatchPat = GetMatchPattern(cmbBoxMatch.SelectedItem.ToString());
                //string ModifiedStr = GetModifiedStringBasedOnMatchPatterns(txtBoxSearchString.Text, SelMatchPat);
                ////DtSearchResult = ObjSearchPatternModel.GetFilteredDataTable(ModifiedStr, "*", cmbBoxSearchIn.SelectedItem.ToString());
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
    
        public  MatchPatterns GetMatchPattern(string SelectedPattern)
        {
            try
            {
                switch (SelectedPattern)
                {
                    case "Starts With":
                        return MatchPatterns.StartsWith;
                    case "Ends With":
                        return MatchPatterns.EndsWith;
                    case "Contains":
                        return MatchPatterns.Contains;
                    case "Equals":
                        return MatchPatterns.Equals;
                    default:
                        return MatchPatterns.StartsWith;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetMatchPattern()", ex);
                return MatchPatterns.StartsWith;
            }
        }
    }

    public enum MatchPatterns
    {
        StartsWith = 0, EndsWith, Contains, Equals
    }

    public class SearchDetails
    {
        public String SearchString, SearchIn;
        public MatchPatterns MatchPattern;
        public Boolean MatchCase;
    }
}
