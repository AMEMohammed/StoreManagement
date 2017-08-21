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
        DBSQL dbsql = new DBSQL();
        int Tag1 = -1;
        int Id = -1;
        DataTable dt1 =new DataTable();
        public frmREPORT()
        {
            InitializeComponent();
        }
        public frmREPORT(int id,int tag)
        {
            InitializeComponent();
            Id = id;
            Tag1 = tag;

        }
        public frmREPORT(int tag, DataTable dt)
        {
            InitializeComponent();
            Tag1 = tag;
           
            dt1 = dt;

        }


        private void frmREPORT_Load(object sender, EventArgs e)
        {
           
            try
            {
                switch (Tag1)
                {
                    case 1:
                        {
                            PrintReSupply(Id);
                            break;
                        }
                    case 2:
                        {
                            PrintReOut(Id);
                            break;
                        }
                    case 3:
                        {
                            PrintSupplyAll(dt1);
                            break;
                        }





                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        ///////////

        public void PrintReOut(int id)
        {


            RPT.RequstOut rt = new RPT.RequstOut();
            rt.SetDataSource(dbsql.PrintRequstOut(id));
            crystalReportViewer1.ReportSource = rt;
            crystalReportViewer1.Refresh();

        }
        ///////
        public void PrintReSupply(int id)
        {

            RPT.RptRqustSupply rt = new RPT.RptRqustSupply();
            rt.SetDataSource(dbsql.PrintRequstSupply((id)));
            crystalReportViewer1.ReportSource = rt;
            crystalReportViewer1.Refresh();
        }
        ////
        public void PrintSupplyAll(DataTable dtt)
        {
          
            RPT.RptRequstSupplyAll rt = new RPT.RptRequstSupplyAll();
            rt.SetDataSource(dtt);
            crystalReportViewer1.ReportSource = rt;
            crystalReportViewer1.Refresh();
        }
    }
}
