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
            this.BackColor = Properties.Settings.Default.colorBackGround;
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();
            label1.BackColor = Properties.Settings.Default.colorBackGround;
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
            this.Cursor = Cursors.WaitCursor;
            if (checkBox1.Checked == true && textBox1.Text == "")
            {
                try
                {
                    dataGridView1.DataSource = dbsql.SearchINRequstOutDate(dateTimePicker1.Value.Date, dateTimePicker2.Value);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
           
         
            else if (textBox1.Text.Length > 0 && checkBox1.Checked == false)
            {
                try { dataGridView1.DataSource = dbsql.SearchINRequsetOuttxt(textBox1.Text); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }

            }
            else if (textBox1.Text == "" && checkBox1.Checked == false)
            {
                try { dataGridView1.DataSource = dbsql.SearchINRequstOutDate(DateTime.Now.AddDays(-3), DateTime.Now); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
                
            }
            else if (textBox1.Text.Length > 0 && checkBox1.Checked == true)
            {
                try { dataGridView1.DataSource = dbsql.SearchINRequsetOutTxtAndDate(textBox1.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
            this.Cursor = Cursors.Default;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = new DataTable();


                ////////// اضافة الاعمدة 
                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count - 1; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int ido = Convert.ToInt32(dr[0].ToString());
                        string nmCa = dr[1].ToString();
                        string nmty = dr[2].ToString();
                        string palce = dr[3].ToString();
                        int Qun = Convert.ToInt32(dr[4].ToString());
                        int prs = Convert.ToInt32(dr[5].ToString());
                        int totl = Convert.ToInt32(dr[6].ToString());
                        string currn = dr[7].ToString();
                        string amer = dr[8].ToString();
                        string astalm = dr[9].ToString(); ;
                        DateTime dd = DateTime.Parse(dr[10].ToString());

                        string dec = dr[11].ToString();
                        string nameUser = dbsql.GetUserNameBYIdUser(Contrl.UserId);
                        dt.Rows.Add(ido, nmCa, nmty, palce, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, amer, astalm, dd.Date.ToShortDateString(), dec," " ,nameUser);
                        this.Cursor = Cursors.Default;
                    }
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    frmREPORT frm = new frmREPORT(4, dt);
                    frm.ShowDialog();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)

            {
                try
                {
                    int IDcheck = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[13].Value.ToString());
                    frmREPORT frm = new frmREPORT(IDcheck, 2);
                    this.Cursor = Cursors.WaitCursor;
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void استردادالطلبToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {

            }
        }

        private void حذفالطلبToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                try
                {
                    int IdOut = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    if (MessageBox.Show("هل تريد استرداد الكمية المصروفة رقم الطلب "+IdOut+"", "استرداد طلب صرف", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes)
                    {
                        dbsql.DeleteRqustOut(IdOut);
                        dataGridView1.DataSource = dbsql.SearchINRequstOutDate(DateTime.Now.AddDays(-3), DateTime.Now);
                    }
                }
                catch(Exception ex)
                { MessageBox.Show(ex.Message);
                }
                    
            }
        }

        private void تعديلالطلبToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    frmUpdtOut2 frmu = new frmUpdtOut2();
                    frmu.Tag = id;
                    this.Cursor = Cursors.WaitCursor;

                    frmu.ShowDialog();
                    this.Cursor = Cursors.Default;
                    dataGridView1.DataSource = dbsql.SearchINRequstOutDate(DateTime.Now.AddDays(-3), DateTime.Now);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
