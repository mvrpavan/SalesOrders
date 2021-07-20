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
    public partial class PleaseWaitForm : Form
    {
        BackgroundWorker ObjBgWorker = null;

        public PleaseWaitForm(String Title, String DialogText, BackgroundWorker bgWorker)
        {
            InitializeComponent();
            Text = Title;
            lblDialogText.Text = DialogText;
            lblDialogText.Focus();
            ObjBgWorker = bgWorker;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ObjBgWorker != null)
                ObjBgWorker.CancelAsync();
        }
    }
}
