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
    public partial class frmUpdtOut2 : Form
    {
        DBSQL dbsql = new DBSQL();
        DataTable dt = new DataTable();
        public frmUpdtOut2()
        {
            InitializeComponent();
        }
        int IdOut = 0;
        private void frmUpdtOut2_Load(object sender, EventArgs e)
        {    IdOut= (int)this.Tag;
            this.BackColor = Properties.Settings.Default.colorBackGround;

            dbsql.GetRequstOutSngle(IdOut);
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
            getDate1();
            ConfDate();
            ////////////
            changeLanguage();
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();
        }
        /////////////////////////////////////////
        ///////////////////
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
            comboBox4.ValueMember = "رقم الجهة";
            comboBox4.DisplayMember = "اسم الجهة";

            comboBox4.DataSource = dbsql.GetAllPlace();

        }
        /////////////////////////////////
        ////////////////

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
        ///////////////////////////////////
        /////////////////
        /////////////////
        public void ConfDate()
        {

            dt = dbsql.GetRequstOutSngle(IdOut);
            comboBox1.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString());
            comboBox2.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDType"].ToString());
            comboBox3.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString());
            comboBox4.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDPlace"].ToString());
            textBox1.Text = dt.Rows[0]["Quntity"].ToString();
            textBox2.Text = dt.Rows[0]["Price"].ToString();
            textBox4.Text = dt.Rows[0]["NameOut"].ToString();
            textBox3.Text = dt.Rows[0]["NameSend"].ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// ///////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {  if ((int)comboBox4.SelectedValue > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                if ((MessageBox.Show("هل تريد ترحيل طلب  تعديل الصرف واعتماده ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {
                    dbsql.UpdateRequstOut(IdOut,(int) comboBox4.SelectedValue, textBox4.Text, textBox3.Text, textBox5.Text,DateTime.Now);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("تاكد من تعبئة جميع الصناديق");
            }

        }
    }
}
