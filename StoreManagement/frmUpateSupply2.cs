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
    public partial class frmUpateSupply2 : Form
    {
        int IDSupply = 0;
        DBSQL dbsql = new DBSQL();
        DataTable dt = new DataTable();
        public frmUpateSupply2()
        {
            InitializeComponent();
            //try
            //{

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    IDSupply = 0;
            //}
        }


        private void frmUpateSupply2_Load(object sender, EventArgs e)
        {
            IDSupply = (int)this.Tag;

            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
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
        public void ConfDate()
        {
            
            dt = dbsql.GetRequstSupply(IDSupply);
            comboBox1.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString());
            comboBox2.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDType"].ToString());
            textBox1.Text = dt.Rows[0]["Quntity"].ToString();
            textBox2.Text = dt.Rows[0]["Price"].ToString();
            textBox4.Text = dt.Rows[0]["NameSupply"].ToString();
            textBox5.Text = dt.Rows[0]["DescSupply"].ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && (int)comboBox1.SelectedValue > 0 && (int)comboBox2.SelectedValue > 0)
            {
                if ((MessageBox.Show("هل تريد ترحيل طلب  تعديل التوريد واعتماده ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {   ////////////////////////////////////
                    // حذف الكمية السابقة من جدول الحسابات
                    try
                    {
                        int oldQuntity = Convert.ToInt32(dt.Rows[0]["Quntity"].ToString());
                        int idAcount2 = dbsql.CheckAccountIsHere(Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()));
                        int QuntityHere = dbsql.GetQuntityInAccount(idAcount2);
                        int qu = QuntityHere - oldQuntity;
                        dbsql.UpdateQuntityAccount(idAcount2, qu);
                        //////////////////////////////////////
                        //// عملية ادخل القيمة الجديدة في الحساب
                        int newQuntity = Convert.ToInt32(textBox1.Text);
                        int NewPrice = Convert.ToInt32(textBox2.Text);
                        int IDCAT = (int)comboBox1.SelectedValue;
                        int IDTYPE = (int)comboBox2.SelectedValue;
                        string nameNEW = textBox4.Text;
                        string decNew = textBox5.Text;
                        int idAcount = dbsql.CheckAccountIsHere(IDCAT, IDTYPE, NewPrice);
                        MessageBox.Show(idAcount.ToString());
                        if (idAcount > 0) // في حالة الحساب موجود من قبل
                        {   //  تعديل الحساب بالكمية الجديدة
                            int oldQunt = dbsql.GetQuntityInAccount(idAcount);
                            MessageBox.Show(oldQunt.ToString() + "الكمية القديمة");
                            int newQunt = oldQunt + newQuntity;
                            MessageBox.Show(newQunt.ToString() + "الكمية الجديد");
                            MessageBox.Show(idAcount.ToString() + "رقم الحساب");
                            dbsql.UpdateQuntityAccount(idAcount, newQunt);


                        }
                        else //  في حالة الحساب جديد
                        {
                            dbsql.AddNewAccount(IDCAT, IDTYPE, newQuntity, NewPrice);// اضافة حساب جديد
                        }
                        /////////////////////////////////
                        ///////////////////////////////////////////////////////////////
                        // عملية التعديل في جدول التوريد
                        dbsql.UPateRequstSupply(IDSupply, IDCAT, IDTYPE, newQuntity, NewPrice, nameNEW, decNew);
                        //////////////////
                        ////////////
                        // عملية الحفظ في جدول التعديلات
                        dbsql.ADDNewUPDSupply(IDSupply, Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), dt.Rows[0]["NameSupply"].ToString(), dt.Rows[0]["DescSupply"].ToString(), DateTime.Parse(dt.Rows[0]["DateSupply"].ToString()), DateTime.Now, decNew);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    this.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
