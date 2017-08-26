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
    public partial class frmSettingUser : Form
    {
        public frmSettingUser()
        {
            InitializeComponent();
        }
        DBSQL dbsql = new DBSQL();
        private void frmSettingUser_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;
            try
            {
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";

                MessageBoxManager.Register();
                DataTable dt = new DataTable();
                dt = dbsql.GetUser();
                textBox1.Text = dt.Rows[0][0].ToString();
                textBox2.Text = dt.Rows[0][1].ToString();


            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {  if ((MessageBox.Show("هل تريد اكمال عملية تعديل البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {
                    try {
                        dbsql.UpdateUser(textBox1.Text, textBox2.Text);
                        this.Close();

                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
        }

        }
    }
}
