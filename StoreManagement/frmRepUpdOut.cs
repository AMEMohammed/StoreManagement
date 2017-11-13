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
    public partial class frmRepUpdOut : Form
    {
        DBSQL dbsql = new DBSQL();
        public frmRepUpdOut()
        {
            InitializeComponent();
        }

        private void frmRepUpdOut_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
           try
            {
                this.Cursor = Cursors.WaitCursor;
                if (textBox1.Text.Length > 0)
                {
                    dataGridView1.DataSource = dbsql.GetUpdtOutByIDOut(Convert.ToInt32(textBox1.Text));
                }
                else if (radioButton1.Checked)
                {
                    if (checkBox1.Checked == true)
                    {
                        dataGridView1.DataSource = dbsql.GetUpdOutByDate(dateTimePicker1.Value, dateTimePicker2.Value);
                    }
                    else
                    {
                        dataGridView1.DataSource = dbsql.GetUpdOutByDate1();
                    }
                    
                   
                }
                else if(radioButton2.Checked)
                {

                    if (checkBox1.Checked == true)
                    {
                        dataGridView1.DataSource = dbsql.GetUpdOutByDateUpdtewithdate(dateTimePicker1.Value, dateTimePicker2.Value);
                    }
                    else
                    {
                        dataGridView1.DataSource = dbsql.GetUpdOutByDateUpdte();
                    }
                }
                else if(radioButton3.Checked)
                {
                    if (checkBox1.Checked == true)
                    {
                        dataGridView1.DataSource = dbsql.GetUpdOutByDateDetle2tewithdate(dateTimePicker1.Value, dateTimePicker2.Value);
                    }
                    else
                    {
                        dataGridView1.DataSource = dbsql.GetUpdOutByDatedelte();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = new DataTable();


                ////////// اضافة الاعمدة 
                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int id = Convert.ToInt32(dr[0].ToString());
                        int idSu = Convert.ToInt32(dr[1].ToString());
                        string NCat = dr[2].ToString();
                        string TypeCA = dr[3].ToString();
                        int Qunit = Convert.ToInt32(dr[4].ToString());
                        int Price = Convert.ToInt32(dr[5].ToString());
                        string currn = dr[6].ToString();
                        string namee = dr[9].ToString();
                        DateTime dd = DateTime.Parse(dr[11].ToString());
                        string dec = dr[10].ToString();
                        string nameUser = dbsql.GetUserNameBYIdUser(Contrl.UserId);
                       
                        dt.Rows.Add(id, idSu, NCat, TypeCA, string.Format("{0:##,##}", Qunit), string.Format("{0:##,##}", Price), currn," "," ", namee, dec,dd.Date.ToShortDateString()," " ,nameUser);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmREPORT frm = new frmREPORT(7, dt);
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// //////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                DataTable dt = new DataTable();


                ////////// اضافة الاعمدة 
                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int id = Convert.ToInt32(dr[0].ToString());
                        int idSu = Convert.ToInt32(dr[1].ToString());
                        string NCat = dr[2].ToString();
                        string TypeCA = dr[3].ToString();
                        int Qunit = Convert.ToInt32(dr[4].ToString());
                        int Price = Convert.ToInt32(dr[5].ToString());
                        string currn = dr[6].ToString();
                        string namee = dr[9].ToString();
                        DateTime dd = DateTime.Parse(dr[11].ToString());
                        string dec = dr[10].ToString();
                        string nameUser = dbsql.GetUserNameBYIdUser(Contrl.UserId);

                        dt.Rows.Add(id, idSu, NCat, TypeCA, string.Format("{0:##,##}", Qunit), string.Format("{0:##,##}", Price), currn, " ", " ", namee, dec, dd.Date.ToShortDateString(), " ", nameUser);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmREPORT frm = new frmREPORT(7, dt);
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
