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
    public partial class frmRestorDatebase : Form
    {
        public frmRestorDatebase()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                if (MessageBox.Show("سوف يتم فقد اي بيانات حفظ بعد انشاء بعد انشاء النسخة هل تريد الاستمرار","تنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes)
                {
                    string pa = textBox1.Text;
                    DBSQL dbsql = new DBSQL(0);
                    dbsql.RestorBackUpDataBase(pa);
                    MessageBox.Show("تمت عملية الاستعادة بنجاح");
                    textBox1.Text = "";
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRestorDatebase_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;
        }
    }
}
