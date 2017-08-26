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
        /// <summary>
        /// //////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0)
            {
                try
                {
                    dbsql.AddNewCurrency(textBox1.Text);
                    dataGridView1.DataSource = dbsql.GetAllCurrency();
                    textBox1.Text = "";
                    textBox1.Focus();               }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        int id = 0;
        /// <summary>
        /// ///////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {  /// عند الدخول الى القائمة
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }
        /// <summary>
        /// //////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text.Length>0)
            {
                try
                {
                    dbsql.UpdateCurrency(id, textBox2.Text);
                    dataGridView1.DataSource = dbsql.GetAllCurrency();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    id = 0;
                }
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }
        /// <summary>
        /// /////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد حذف العملة المحددة مع جميع الارتباطات ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
            {
                try
                {
                    dbsql.DeleteCurrency(id);
                    dataGridView1.DataSource = dbsql.GetAllCurrency();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    id = 0;
                }
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }
            }
         }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCurrency_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Properties.Settings.Default.colorBackGround;
                changeLanguage();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
                dataGridView1.DataSource = dbsql.GetAllCurrency();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            try
            {
                dataGridView1.DataSource = dbsql.GetAllCurrency();
                textBox1.Text = "";
                textBox2.Text = "";
                id = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {  try
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }
    }
}
