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
        {  if(checkUser==true)
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

        }
    }
}
