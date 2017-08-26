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
    public partial class frmRPTOut : Form
    {
        DBSQL dbsql = new DBSQL();
        public frmRPTOut()
        {
            InitializeComponent();
        }

        private void frmRPTOut_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;
            try
            {/////////
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
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
        /// <summary>
        /// /////
        /// </summary>
        void getDate1()
        {
            try
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

                comboBox4.ValueMember = "رقم الجهة";
                comboBox4.DisplayMember = "اسم الجهة";
                comboBox4.DataSource = dbsql.GetAllPlace();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //////////////////////////////////
        public void changeLanguage()
        {
            foreach (InputLanguage lng in InputLanguage.InstalledInputLanguages)
            {
                if (lng.LayoutName == "العربية (101)")
                    InputLanguage.CurrentInputLanguage = lng;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string idca = "";
            string idty = "";
            string currn = "";
            string place = "";
            string namee ="";
            if(checkBox1.Checked==false)
            {try { idca = comboBox1.SelectedValue.ToString(); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }

            }
            if (checkBox2.Checked == false)
            {
                try { idty = comboBox2.SelectedValue.ToString(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
            if (checkBox3.Checked == false)
            {
                try { currn = comboBox3.SelectedValue.ToString(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
            if (checkBox5.Checked == false)
            {
                try { place = comboBox4.SelectedValue.ToString(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
            if(textBox1.Text.Length>0)
            {
               namee = textBox1.Text;
            }
            if(checkBox4.Checked)
            {
                try { dataGridView1.DataSource = dbsql.PrintOutAllwithDate(idca, idty, place, currn, namee, dateTimePicker1.Value.Date, dateTimePicker2.Value); }
                catch(Exception ex) { MessageBox.Show(ex.Message); }

            }
            else 
            {
                try { dataGridView1.DataSource = dbsql.PrintOutAll(idca, idty, place, currn, namee); }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==false)
            {
                comboBox1.Enabled = true;

            }
            else
            {
                comboBox1.Enabled = false;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            {
                comboBox2.Enabled = true;

            }
            else
            {
                comboBox2.Enabled = false;

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == false)
            {
                comboBox3.Enabled = true;

            }
            else
            {
                comboBox3.Enabled = false;

            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == false)
            {
                comboBox4.Enabled = true;

            }
            else
            {
                comboBox4.Enabled = false;

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
                dateTimePicker2.Enabled = false; ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>0)
            {
                
                
                    DataTable dt = new DataTable();

                ////////// اضافة الاعمدة 
                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                }
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
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
                        dt.Rows.Add(ido, nmCa, nmty, palce, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, amer, astalm, dd.Date.ToShortDateString(), dec);
                        this.Cursor = Cursors.Default;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmREPORT frm = new frmREPORT(4, dt);
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                }


            }
        
    }
    }


