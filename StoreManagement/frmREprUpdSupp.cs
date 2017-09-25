using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StoreManagement
{
    public partial class frmREprUpdSupp : Form
    {
        DBSQL dbsql = new DBSQL();
        public frmREprUpdSupp()
        {
            InitializeComponent();
        }

        private void frmREprUpdSupp_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        ///  search 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length > 0)
                {
                    dataGridView1.DataSource = dbsql.GetUpdateSupplyByIDSupply(Convert.ToInt32(textBox1.Text));
                }
                else if (checkBox1.Checked == true)
                {
                    dataGridView1.DataSource = dbsql.GetUpdateSupplyByDate(dateTimePicker1.Value, dateTimePicker2.Value);
                }
                else
                {
                    dataGridView1.DataSource = dbsql.GetUpdateSupplyByDate(DateTime.Now.AddDays(-7), DateTime.Now);
                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message);
            }
        }
    }
}
