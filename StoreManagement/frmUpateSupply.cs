using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace StoreManagement
{
    public partial class frmUpateSupply : Form
    { DBSQL dbsql = new DBSQL();
        public frmUpateSupply()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
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

        private void frmUpateSupply_Load(object sender, EventArgs e)
        {
            try
              {

            dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-7), DateTime.Now);

             }
             catch(Exception ex)
      { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true && textBox1.Text == "")
            {

                dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(dateTimePicker1.Value.Date, dateTimePicker2.Value);

            }

            else if (textBox1.Text.Length > 0 && checkBox1.Checked == false)
            {
                dataGridView1.DataSource = dbsql.SearchINRequsetSupply(textBox1.Text);

            }
            else if (textBox1.Text == "" && checkBox1.Checked == false)
            {
                dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-7), DateTime.Now);
            }
            else if (textBox1.Text.Length > 0 && checkBox1.Checked == true)
            {
                dataGridView1.DataSource = dbsql.SearchINRequsetSupplyTxtAndDate(textBox1.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value);
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           if(textBox1.Text.Length>0 && checkBox1.Checked==false)
                dataGridView1.DataSource = dbsql.SearchINRequsetSupply(textBox1.Text);
          else if (textBox1.Text == "" && checkBox1.Checked == false)
            {
                dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-7), DateTime.Now);
            }
          else if(textBox1.Text.Length>0 && checkBox1.Checked==true)
            {
                dataGridView1.DataSource = dbsql.SearchINRequsetSupplyTxtAndDate(textBox1.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value);

            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                frmUpateSupply2 frmu = new frmUpateSupply2();
                frmu.Tag = id;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                this.Cursor = Cursors.WaitCursor;
                frmREPORT frm = new frmREPORT(id, 1);
                frm.ShowDialog();
                this.Cursor = Cursors.Default;
            }

        }
         /// <summary>
         ///  print all
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataSet1 ds = new DataSet1();
                DataTable dt = new DataTable();
                dt= (DataTable)dataGridView1.DataSource;                            
                this.Cursor = Cursors.WaitCursor;
                frmREPORT frm = new frmREPORT(3, dt);
                frm.ShowDialog();
                this.Cursor = Cursors.Default;


            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///////////////
    }
}
