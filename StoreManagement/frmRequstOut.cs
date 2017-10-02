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
    public partial class frmRequstOut : Form
    { DBSQL dbsql = new DBSQL();
        
             
        public frmRequstOut()
        {
            InitializeComponent();
        }

        private void frmRequstOut_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;
            try
            {
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                GetData1();
                changeLanguage();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";

                MessageBoxManager.Register();
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }
////////////////////////////////////////////////////////////////////////////////////////////////
        void GetData1()
        {
           try
            {
                
                comboBox1.ValueMember = "رقم الصنف";
                comboBox1.DisplayMember = "اسم الصنف";
                comboBox1.DataSource = dbsql.GetCatagoryInAccount();
                
                comboBox3.ValueMember = "رقم الجهة";
                comboBox3.DisplayMember = "اسم الجهة";
                comboBox3.DataSource = dbsql.GetAllPlace();
             
                dataGridView1.DataSource = dbsql.SearchINRequstOutDate(DateTime.Now.Date, DateTime.Now); // جلب طلبات الصرف لليوم الحالي
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[12].Visible = false;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

  /////////////////////////////////////////////////////////////////////////////////////////////////////   
        public void GetDate2()
        {
            try
            {
                comboBox2.DisplayMember = "اسم النوع";
                comboBox2.ValueMember = "رقم النوع";
                comboBox2.DataSource = dbsql.GetTypeInAccount((int)comboBox1.SelectedValue);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        ////////////////////////////////////////////////
        private void comboBox1_Leave(object sender, EventArgs e)
        {
            GetDate2();
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

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            try
            {
             
                comboBox4.ValueMember = "رقم العملة";
                comboBox4.DisplayMember = "اسم العملة";
                comboBox4.DataSource = dbsql.GetCurrencyINAccount((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                try
                {
                    int qunntNow = dbsql.GetQunitiyinAccount2((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue,(int)comboBox4.SelectedValue);
                    int qunnMus = Convert.ToInt32(textBox2.Text);
                    if (qunnMus > qunntNow)
                    {
                        MessageBox.Show("الكمية لا تسمح");
                        textBox2.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            { textBox2.Focus(); }
        }
        /// <summary>
        /// //////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((int)comboBox1.SelectedValue > 0 & (int)comboBox2.SelectedValue > 0 & (int)comboBox3.SelectedValue > 0 & (int)comboBox4.SelectedValue > 0 & textBox2.Text.Length > 0 & textBox3.Text.Length > 0 & textBox4.Text.Length > 0)
                {
                    if ((MessageBox.Show("هل تريد ترحيل طلب الصرف ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                    {
                        int Idcat = ((int)comboBox1.SelectedValue);
                        int idtyp = ((int)comboBox2.SelectedValue);
                        int idplace = ((int)comboBox3.SelectedValue);
                        int idcurrnt = ((int)comboBox4.SelectedValue);
                        int QunntyMust = Convert.ToInt32(textBox2.Text);
                        string nameAmmer = textBox3.Text;
                        string nameMostlaem = textBox4.Text;
                        string Decrip = textBox5.Text;



                        DataTable dtAccountIDs = new DataTable();
                        dtAccountIDs = dbsql.GetAccountIDs(Idcat, idtyp, idcurrnt); // [جلب الحسابات التي تحتوي على نفس النوع والصنف
                        int MaxCheckRequstOut = dbsql.GetMaxCheckInRequsetOut();
                        MaxCheckRequstOut += 1;

                        int Quntity2 = QunntyMust;

                        for (int i = 0; i < dtAccountIDs.Rows.Count; i++)
                        {
                            int IDAccount = Convert.ToInt32(dtAccountIDs.Rows[i][0].ToString());
                            int result = dbsql.GetAndCheckQuntityAccountAndAddRqustNew(IDAccount, Quntity2, Idcat, idtyp, idcurrnt, idplace, nameAmmer, Decrip, DateTime.Now, MaxCheckRequstOut, nameMostlaem);
                          

                            if (result == 0)
                            {
                                break;

                            }
                            else
                            {

                                Quntity2 -= result;

                            }


                        }
                        if ((MessageBox.Show("هل تريد طباعة سند صرف؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                        {
                            frmREPORT frm = new frmREPORT(MaxCheckRequstOut, 2);

                            frm.ShowDialog();

                        }

                        refrsh1();



                    }



                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        //////// refersh
        void refrsh1()
        {
            GetData1();
            GetDate2();
            GetDate3();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            button3.Focus();
            
        }
        public void GetDate3()
        {
            try
            {

                comboBox4.ValueMember = "رقم العملة";
                comboBox4.DisplayMember = "اسم العملة";
                comboBox4.DataSource = dbsql.GetCurrencyINAccount((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refrsh1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)

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

        private void comboBox4_Leave(object sender, EventArgs e)
        {
            try {
                textBox1.Text = dbsql.GetQunitiyinAccount2((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue, (int)comboBox4.SelectedValue).ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = dbsql.GetQunitiyinAccount2((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue, (int)comboBox4.SelectedValue).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
