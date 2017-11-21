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
    public partial class frmMain : Form
    { DBSQL dbsql = new DBSQL();
        public static bool checkUser = false;

        public frmMain()
        {
            new Form1().ShowDialog();
            
            InitializeComponent();
        }

        private void الخروجمنالنظامToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            if ((MessageBox.Show("هل تريد عمل نسخة احتياطية قبل الخروج من النظام ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
            {
                try
                {
                    string path = Properties.Settings.Default.pathbukup + "\\StoreManagement" + DateTime.Now.ToShortDateString().Replace('/', '-') + DateTime.Now.ToShortTimeString().Replace(':', '-') + ".bak";
                    dbsql.BuckUpdatabase(path);
                    MessageBox.Show("تمت عملية انشاء نسخة احتياطية بنجاح");
                    Application.Exit();
                }
                catch
                {
                    MessageBox.Show("خطا في عملية النسخ الاحتياطي");
                    Application.Exit();
                }
            }
           else
            {
                Application.Exit();
            }
        }

       void checkIcon(bool check)
        {

            //    this.اعدادتالنظامToolStripMenuItem.Enabled = check;
            try
            {
                if (Contrl.UserId == 770958747)
                {


                    this.التقاريرToolStripMenuItem.Visible = true;
                    this.تهيئةالنظامToolStripMenuItem.Visible = true;
                    this.طلباتالتوريدToolStripMenuItem.Visible = true;
                    this.طلباتالصرفToolStripMenuItem.Visible = true;
                    this.اضافةطلبToolStripMenuItem.Visible = true;
                    this.اضافةطلبToolStripMenuItem1.Visible = true;
                    this.البحثToolStripMenuItem.Visible= true;
                    this.بحثToolStripMenuItem.Visible = true;
                    this.استعادةالنسخةالاحتياطيةToolStripMenuItem.Visible = true;
                    this.انشاءنسخةاحتياطيةToolStripMenuItem.Visible = true;
                    this.اضافةطلبToolStripMenuItem.Visible = true;
                    this.تعديلالمستخدمToolStripMenuItem.Visible = true;
                    this.اعدادتالنظامToolStripMenuItem.Visible = true;
                    this.صلاحياتالمستخدمينToolStripMenuItem.Visible = true;
                    this.فترةالصلاحيةToolStripMenuItem.Visible = true;                }
                else
                {
                    DataTable dt2 = new DataTable();
                    dt2 = dbsql.GetGrendUsers(Contrl.UserId);

                    this.التقاريرToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][7].ToString());
                    this.تهيئةالنظامToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][4].ToString());
                    this.طلباتالتوريدToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][4].ToString());
                    this.طلباتالصرفToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][5].ToString());
                    this.اضافةطلبToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][4].ToString());
                    this.اضافةطلبToolStripMenuItem1.Visible = Convert.ToBoolean(dt2.Rows[0][5].ToString());
                    this.البحثToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][6].ToString());
                    this.بحثToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][6].ToString());
                    this.استعادةالنسخةالاحتياطيةToolStripMenuItem.Visible = false;
                    this.فترةالصلاحيةToolStripMenuItem.Visible = false;
                    this.انشاءنسخةاحتياطيةToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][4].ToString()); 
                    this.اضافةطلبToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][4].ToString());
                    this.تعديلالمستخدمToolStripMenuItem.Visible = check;
                    this.اعدادتالنظامToolStripMenuItem.Visible = check;
                    this.صلاحياتالمستخدمينToolStripMenuItem.Visible = Convert.ToBoolean(dt2.Rows[0][8].ToString());
                }
                if (check == false)
                {
                    Contrl.UserId = 0;
                }
            }
            catch
            { }
         
        }
        /////
        void checkIcon2(bool check)
        {

            //    this.اعدادتالنظامToolStripMenuItem.Enabled = check;
            try
            {
              

                this.التقاريرToolStripMenuItem.Visible = check;
                this.تهيئةالنظامToolStripMenuItem.Visible = check;
                this.طلباتالتوريدToolStripMenuItem.Visible = check;
                this.طلباتالصرفToolStripMenuItem.Visible = check;
                this.اضافةطلبToolStripMenuItem.Visible = check;
                this.اضافةطلبToolStripMenuItem1.Visible = check;
                this.البحثToolStripMenuItem.Visible = check;
                this.بحثToolStripMenuItem.Visible = check;
                this.استعادةالنسخةالاحتياطيةToolStripMenuItem.Visible = false;
                this.انشاءنسخةاحتياطيةToolStripMenuItem.Visible = check;
                this.تعديلالمستخدمToolStripMenuItem.Visible = check;
                this.اعدادتالنظامToolStripMenuItem.Visible = true;
                this.الاتصالبالسيرفيرToolStripMenuItem.Visible = true;
                this.صلاحياتالمستخدمينToolStripMenuItem.Visible = check;
                if (check == false)
                {
                    Contrl.UserId = 0;
                }
            }
            catch
            { }

        }
        private void دخولالنظامToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmlogin().ShowDialog();

            if (checkUser==true)
            {   

                checkIcon(true);
            }
           else
            {
                checkIcon(false);
            }
        }
        /// <summary>
        /// ////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;

                MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";

            MessageBoxManager.Register();
         

        }

        private void خروجمنالنظامToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkIcon2(false);
            checkUser = false;


        }

      

        private void اضافةطلبToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmSupplyRequset().ShowDialog();
            this.Cursor = Cursors.Default;

        }

        private void البحثToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmUpateSupply().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void اضافةطلبToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmRequstOut().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void بحثToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmUpateOut().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void الاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmCategory().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void انواعالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmTypeQuntity().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void العملاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmCurrency().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void الجهاتالمستفيدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmPlaceSend().ShowDialog();
            this.Cursor = Cursors.Default;
        }

       

        private void تقاريرالتعديلاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmRPTSupply().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void شعلرالتقاريرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmsttingImage().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void تقاريرالمخزونToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmRPTAccount().ShowDialog();
            this.Cursor = Cursors.Default;

        }

        private void تقاريرالصرفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmRPTOut().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void انشاءنسخةاحتياطيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmBuckUpDatabase().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void استعادةالنسخةالاحتياطيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmRestorDatebase().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void الاتصالبالسيرفيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmsttingImage().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void تعديلالمستخدمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmSettingUser().ShowDialog();
            this.Cursor = Cursors.Default;

        }

        private void تقاريرالتعديلاتToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            new frmREprUpdSupp().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void تقاريرتعديلاتالصرفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmRepUpdOut().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void صلاحياتالمستخدمينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmUser().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void دخولالنظامToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void انواعالحساباتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmTypeAccount().ShowDialog();
            this.Cursor = Cursors.Default;

        }

        private void فترةالصلاحيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmEnd().ShowDialog();
            this.Cursor = Cursors.Default;
        }
    }
}
