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
    public partial class frmRPTSupply : Form
    {
        DBSQL dbsql = new DBSQL();
        public frmRPTSupply()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmRPTSupply_Load(object sender, EventArgs e)
        {
            try
            {/////////
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                getDate1();
                ////////////
                changeLanguage();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
                ///////

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void getDate1()
        {
            comboBox1.DisplayMember = "اسم الصنف";
            comboBox1.ValueMember = "رقم الصنف";
            comboBox1.DataSource = dbsql.GetAllCategoryAR();
            comboBox2.DisplayMember = "اسم النوع";
            comboBox2.ValueMember = "رقم النوع";
            comboBox2.DataSource = dbsql.GetAllTypeQuntity();
            comboBox3.DisplayMember = "اسم العملة";
            comboBox3.ValueMember = "رقم العملة";
            comboBox3.DataSource = dbsql.GetAllCurrency();
          
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                comboBox1.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                comboBox2.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                comboBox3.Enabled = false;
            }
            else
            {
                comboBox3.Enabled = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox4.Checked==true)
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

        private void button4_Click(object sender, EventArgs e)
        {
            string IDCat ="";
            string IDTyp ="";
            string IDCurrn ="";
            string Name ="";
            if(checkBox1.Checked==false)
            {
                IDCat = comboBox1.SelectedValue.ToString();

            }
            if(checkBox2.Checked==false)
            {
                IDTyp = comboBox2.SelectedValue.ToString();
            }
            if(checkBox3.Checked==false)
            {
                IDCurrn = comboBox3.SelectedValue.ToString();
            }
            if(textBox4.Text.Length>0)
            {
                Name = textBox4.Text;
            }
            this.Cursor = Cursors.WaitCursor;
            if (checkBox4.Checked ==false)
            {
                dataGridView1.DataSource = dbsql.PrintRequstRPT(IDCat, IDTyp, IDCurrn, Name);
            }
            else
            {
                dataGridView1.DataSource = dbsql.PrintRequstRPT(dateTimePicker1.Value.Date,dateTimePicker2.Value,IDCat, IDTyp, IDCurrn, Name);
            }
            this.Cursor = Cursors.Default;
           

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataTable dt = new DataTable();


                ////////// اضافة الاعمدة 
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                }
                ///////////////////  أاضافة سطور 
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
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
                    dt.Rows.Add(idS, nmCa, nmty, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, dd.Date.ToShortDateString(), namee, dec);
                }

                this.Cursor = Cursors.WaitCursor;
                frmREPORT frm = new frmREPORT(3, dt);
                frm.ShowDialog();
                this.Cursor = Cursors.Default;
            }
        }
    }
}
