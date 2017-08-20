using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagement
{
    public partial class frmREPORT : Form
    {
        public frmREPORT()
        {
            InitializeComponent();
        }
        DBSQL dbsql = new DBSQL();
        private void frmREPORT_Load(object sender, EventArgs e)
        {
            try
            {
                int id = (int)this.Tag;
                // RPT.RPTrequstSupply rt = new RPT.RPTrequstSupply();
                //dbsql.RPTRqustSupply(id);
                RPT.RequstOut rt = new RPT.RequstOut();
                rt.SetDataSource(dbsql.PrintRequstOut(id));
                crystalReportViewer1.ReportSource = rt;
                crystalReportViewer1.Refresh();
              

               // rt.SetDataSource(dbsql.RPTRqustSupply(id));
              //  crystalReportViewer1.ReportSource = rt;
               // crystalReportViewer1.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
