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
    public partial class frmUpateOut : Form
    {
        DBSQL dbsql = new DBSQL();
        public frmUpateOut()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void frmUpateOut_Load(object sender, EventArgs e)
        {
            try
            {
                changeLanguage();
                dataGridView1.DataSource = dbsql.SearchINRequstOutDate(DateTime.Now.AddDays(-3), DateTime.Now);

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }
        ///////////////
        /// <summary>
        /// // change language
        /// </summary>
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && textBox1.Text == "")
            {

                dataGridView1.DataSource = dbsql.SearchINRequstOutDate(dateTimePicker1.Value.Date, dateTimePicker2.Value);

            }

            else if (textBox1.Text.Length > 0 && checkBox1.Checked == false)
            {
                dataGridView1.DataSource = dbsql.SearchINRequsetOuttxt(textBox1.Text);

            }
            else if (textBox1.Text == "" && checkBox1.Checked == false)
            {
                dataGridView1.DataSource = dbsql.SearchINRequstOutDate(DateTime.Now.AddDays(-3), DateTime.Now);
            }
            else if (textBox1.Text.Length > 0 && checkBox1.Checked == true)
            {
                dataGridView1.DataSource = dbsql.SearchINRequsetOutTxtAndDate(textBox1.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text.Length > 0 && checkBox1.Checked == false)
                dataGridView1.DataSource = dbsql.SearchINRequsetOuttxt(textBox1.Text);
            else if (textBox1.Text == "" && checkBox1.Checked == false)
            {
                dataGridView1.DataSource = dbsql.SearchINRequstOutDate(DateTime.Now.AddDays(-7), DateTime.Now);
            }
            else if (textBox1.Text.Length > 0 && checkBox1.Checked == true)
            {
                dataGridView1.DataSource = dbsql.SearchINRequsetOutTxtAndDate(textBox1.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value);

            }
        }
    }
}
