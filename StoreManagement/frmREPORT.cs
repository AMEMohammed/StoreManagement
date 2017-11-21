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
        bool printexit = false;
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
        public frmREPORT(int id, int tag,int userid,bool print1)
        {
            InitializeComponent();
            Id = id;
            Tag1 = tag;
            user = userid;
            printexit = print1;

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
                    case 7:
                        {
                            PrintUpdteOUt(dt1);
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
        
           crystalReportViewer1.Refresh();
            if (printexit == true)
            {
                DataTable dtt1 = new DataTable();
                RPT.ExitStatement rt1 = new RPT.ExitStatement();
                dtt1 = dbsql.printrequstOutExit(id, Contrl.UserId, user);
            
                for (int i = 0; i < dtt1.Rows.Count; i++)
                {
                   dtt1.Rows[i][6] = "تصريح خروج مواد";
                }
                rt1.SetDataSource(dtt1);
                  rt1.PrintToPrinter(1, false, 0, 0); //print dicret
               
            }

        }
        ///////
        public void PrintReSupply(int id)
        {

            RPT.RptRqustSupply rt = new RPT.RptRqustSupply();                          
            rt.SetDataSource(dbsql.PrintRequstSupply(id,Contrl.UserId,user));
            crystalReportViewer1.ReportSource = rt;
            
            crystalReportViewer1.Refresh();
            if (printexit == true)
            {
                DataTable dtt1 = new DataTable();
                RPT.ExitStatement rt1 = new RPT.ExitStatement();
                dtt1 = dbsql.printrequstOutExit1(id, Contrl.UserId, user);
                for(int i=0;i<dtt1.Rows.Count;i++)
                {
                    dtt1.Rows[i][4] = "مسؤول المخازن";
                    dtt1.Rows[i][6] = "تصريح توريد مخزني";
                    dtt1.Rows[i][7] = "المخازن";
                }
                rt1.SetDataSource(dtt1);
                rt1.PrintToPrinter(1, false, 0, 0); //print dicret
           
               

            }
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
        public void PrintUpdteOUt(DataTable dt11)
        {
            RPT.UpdOut updSupp = new RPT.UpdOut();
            updSupp.SetDataSource(dt11);
            crystalReportViewer1.ReportSource = updSupp;
            crystalReportViewer1.Refresh();

        }

    }
}
