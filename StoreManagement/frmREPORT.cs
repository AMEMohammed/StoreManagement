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
        int user = 0;
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
        public frmREPORT(int id, int tag,int userid)
        {
            InitializeComponent();
            Id = id;
            Tag1 = tag;
            user = userid;

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
                    case 4:
                        {
                            printOutALL(dt1);
                            break;
                        }
                    case 5:
                        {
                            PrintAccountQuntity(dt1);
                            break;
                        }
                    case 6:
                        {
                            PrintUpdteSupply(dt1);
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
            DataTable dtttt = new DataTable();
            dtttt = dbsql.PrintRequstOut(id,Contrl.UserId,user);
         
            rt.SetDataSource(dtttt); 

            crystalReportViewer1.ReportSource = rt;
       //   rt.PrintToPrinter(1, false, 0, 0); //print dicret
            crystalReportViewer1.Refresh();

        }
        ///////
        public void PrintReSupply(int id)
        {

            RPT.RptRqustSupply rt = new RPT.RptRqustSupply();                          
            rt.SetDataSource(dbsql.PrintRequstSupply(id,Contrl.UserId,user));
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
        public void printOutALL(DataTable dttt)
        {
            RPT.OutAll outall = new RPT.OutAll();
            outall.SetDataSource(dttt);
            crystalReportViewer1.ReportSource = outall;
            crystalReportViewer1.Refresh();
        }

        public void PrintAccountQuntity(DataTable ddd)
        {
            RPT.AccountQintity outall = new RPT.AccountQintity();
            outall.SetDataSource(ddd);
            crystalReportViewer1.ReportSource = outall;
            crystalReportViewer1.Refresh();
        }
        public void PrintUpdteSupply(DataTable dt11)
        {
            RPT.UpdSupply updSupp = new RPT.UpdSupply();
            updSupp.SetDataSource(dt11);
            crystalReportViewer1.ReportSource = updSupp;
            crystalReportViewer1.Refresh();
        }
    }
}
