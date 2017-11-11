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

        private void frmUpateSupply_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;
            label1.BackColor = Properties.Settings.Default.colorBackGround;

            try
            {
                changeLanguage();
                dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-3), DateTime.Now);

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true && textBox1.Text == "")
            {

                try { dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(dateTimePicker1.Value.Date, dateTimePicker2.Value); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }

            else if (textBox1.Text.Length > 0 && checkBox1.Checked == false)
            {
                try { dataGridView1.DataSource = dbsql.SearchINRequsetSupply(textBox1.Text); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
            else if (textBox1.Text == "" && checkBox1.Checked == false)
            {
                try { dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-3), DateTime.Now); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (textBox1.Text.Length > 0 && checkBox1.Checked == true)
            {
                try
                { dataGridView1.DataSource = dbsql.SearchINRequsetSupplyTxtAndDate(textBox1.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }


        }
      
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && checkBox1.Checked == false)
                try { dataGridView1.DataSource = dbsql.SearchINRequsetSupply(textBox1.Text); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            else if (textBox1.Text == "" && checkBox1.Checked == false)
            {
                try { dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-7), DateTime.Now); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (textBox1.Text.Length > 0 && checkBox1.Checked == true)
            {
                try { dataGridView1.DataSource = dbsql.SearchINRequsetSupplyTxtAndDate(textBox1.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }

            }

        }



        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    frmUpateSupply2 frmu = new frmUpateSupply2();
                    frmu.Tag = id;
                    this.Cursor = Cursors.WaitCursor;

                    frmu.ShowDialog();
                    this.Cursor = Cursors.Default;
                    dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-7), DateTime.Now);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[11].Value.ToString());
                    string name = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    bool printExit = false;
                    if (checkBox2.Checked)
                    {
                        printExit = true;
                    }
                    else
                    {
                        printExit = false;
                    }
                 

                    frmREPORT frm = new frmREPORT(id, 1,dbsql.GetIdUser(name),printExit);
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch(Exception ex) { MessageBox.Show( ex.Message); }
            }

        }
        /// <summary>
        ///  print all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
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
                        int idS = Convert.ToInt32(dr[0].ToString());
                        string nmCa = dr[1].ToString();
                        string nmty = dr[2].ToString();
                        int Qun = Convert.ToInt32(dr[3].ToString());
                        int prs = Convert.ToInt32(dr[4].ToString());
                        int totl = Convert.ToInt32(dr[5].ToString());
                        string currn = dr[6].ToString();
                        DateTime dd = DateTime.Parse(dr[7].ToString());
                        string namee = dr[8].ToString();
                        string dec = dr[9].ToString();
                        string nameUser = dbsql.GetUserNameBYIdUser(Contrl.UserId);
                     
                        dt.Rows.Add(idS, nmCa, nmty, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, dd.Date.ToShortDateString(), namee, dec," ",nameUser);


                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmREPORT frm = new frmREPORT(3, dt);
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            { try
                {
                    if ((MessageBox.Show("هل تريد ترحيل طلب  حذف التوريد واعتماده ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))

                    {/// جلب رقم الطلب 
                        int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        DataTable dt = new DataTable();
                        /// جلب بيانات الطلب
                        dt = dbsql.GetRequstSupply(id);
                        int oldQuntity = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                        int idAcount2 = dbsql.CheckAccountIsHere(Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()));
                    
                        int QuntityHere = dbsql.GetQuntityInAccount(idAcount2);
                        if (QuntityHere >= oldQuntity)
                        {
                            // حذف الطلب
                            int qu = QuntityHere - oldQuntity;
                            dbsql.UpdateQuntityAccount(idAcount2, qu); // تعديل الكمية في جدول المخزون
                            //اضافة الطلب في جدول التعديلات
                            dbsql.ADDNewUPDSupply(id, Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()), dt.Rows[0]["NameSupply"].ToString(), DateTime.Parse(dt.Rows[0]["DateSupply"].ToString()), DateTime.Now, "تم حذف الطلب",Contrl.UserId);

                            dbsql.DeleteRequstSupply(id); //حذف الطلب من جدول الطلبات
                            dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-7), DateTime.Now);

                        }
                        else
                        { MessageBox.Show("تاكد من الكيمة المخزونة"); }
                    }
                }
              catch (Exception ex)
                {
                  MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
