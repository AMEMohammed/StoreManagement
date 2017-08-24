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
    {
        public static bool checkUser = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void الخروجمنالنظامToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد الخروج من النظام ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
            {
                Application.Exit();
            }
        }

       void checkIcon(bool check)
        {
           
            this.اعدادتالنظامToolStripMenuItem.Enabled = check;
            this.التقاريرToolStripMenuItem.Enabled = check;
            this.تهيئةالنظامToolStripMenuItem.Enabled = check;
            this.طلباتالتوريدToolStripMenuItem.Enabled = check;
            this.طلباتالصرفToolStripMenuItem.Enabled = check;

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

                MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";

            MessageBoxManager.Register();
            checkIcon(false);
        }

        private void خروجمنالنظامToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkIcon(false);
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
    }
}
