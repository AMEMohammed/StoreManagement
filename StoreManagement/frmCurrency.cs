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
    public partial class frmCurrency : Form
    { DBSQL dbsql = new DBSQL();
        public frmCurrency()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0)
            {
                dbsql.AddNewCurrency(textBox1.Text);
            dataGridView1.DataSource=    dbsql.GetAllCurrency();
                textBox1.Text = "";

            }
        }
        int id = 0;
        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text.Length>0)
            {
                dbsql.UpdateCurrency(id, textBox2.Text);
                dataGridView1.DataSource = dbsql.GetAllCurrency();
                
                textBox1.Text = "";
                textBox2.Text = "";
                id = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد حذف العملة المحددة مع جميع الارتباطات ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
            {
                dbsql.DeleteCurrency(id);
                dataGridView1.DataSource = dbsql.GetAllCurrency();
                textBox1.Text = "";
                textBox2.Text = "";
                id = 0;
            }
         }

        private void frmCurrency_Load(object sender, EventArgs e)
        {
            changeLanguage();
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();
            dataGridView1.DataSource = dbsql.GetAllCurrency();
        }
        public void changeLanguage()
        {
            try
            {
                foreach (InputLanguage lng in InputLanguage.InstalledInputLanguages)
                {
                    if (lng.LayoutName == "العربية (101)")
                        InputLanguage.CurrentInputLanguage = lng;
                }
            }
            catch
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dbsql.GetAllCurrency();
            textBox1.Text = "";
            textBox2.Text = "";
            id = 0;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();


            }
        }
    }
}
