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
    public partial class frmSupplyRequset : Form
    { DBSQL dbsql = new DBSQL();

        public frmSupplyRequset()
        { 
            InitializeComponent();
        }
    
        private void frmSupplyRequset_Load(object sender, EventArgs e)
        {

            getDate1("");
            GetDate2();
            changeLanguage();
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
           
            MessageBoxManager.Register();

           // AuotComp("");
        }


        void getDate1( string s)
        {
            comboBox1.DisplayMember = "اسم الصنف";
            comboBox1.ValueMember = "رقم الصنف";
            comboBox1.DataSource = dbsql.SearchCategory(s);

        } 
        void AuotComp( string s)
        {
            AutoCompleteStringCollection aut = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            dt = dbsql.SearchCategory(s);
            for(int i=0;i<dt.Rows.Count;i++)
            {
                aut.Add(dt.Rows[i][1].ToString());
            }
            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource  =AutoCompleteSource.CustomSource;
            comboBox1.AutoCompleteCustomSource = aut;
        }
        void GetDate2()
        {
            DataTable dt = new DataTable();
            dt = dbsql.GetAllTypeQuntity();
            comboBox2.DisplayMember = "اسم النوع";
            comboBox2.ValueMember = "رقم النوع";
            comboBox2.DataSource = dt;

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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {  
            if(textBox1.Text.Length>0&&textBox2.Text.Length>0&&textBox3.Text.Length>0 &&comboBox1.Text.Length>0)
            { if ((MessageBox.Show("هل تريد ترحيل طلب التوريد واعتماده ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {
                    int idcate = (int)comboBox1.SelectedValue;
                    MessageBox.Show(idcate.ToString());
                    int idtype = (int)comboBox2.SelectedValue;
                    int qunt = Convert.ToInt32(textBox1.Text);
                    int price = Convert.ToInt32(textBox2.Text);
                    int lessQu = Convert.ToInt32(textBox3.Text);
                    string name = textBox4.Text;
                    string dec = textBox5.Text;
                    int idAcount = dbsql.CheckAccountIsHere(idcate, idtype, price);
                    MessageBox.Show(idAcount.ToString());
                    int idCheck = dbsql.CheckQuntityISHereInCheckQuntity(idcate, idtype);
                    if (idAcount > 0) // في حالة الحساب موجود من قبل
                    {   //  تعديل الحساب بالكمية الجديدة
                        int oldQunt = dbsql.GetQuntityInAccount(idAcount);
                        int newQunt = oldQunt + qunt;
                        dbsql.UpdateQuntityAccount(idAcount, newQunt);


                    }
                    else //  في حالة الحساب جديد
                    {
                        dbsql.AddNewAccount(idcate,idtype,qunt,price);// اضافة حساب جديد
                    }
                    /////////////////////////////////
                    if (idCheck > 0)//التاكد من جدول التاكد من الكميات
                    {
                        int oldchqu = dbsql.GetQuntityInChackQuntity(idCheck);
                        int newckqu = oldchqu + qunt;
                        dbsql.UpadateQintityInchekQuntity(idCheck, newckqu);
                    }
                    else
                    {
                        dbsql.AddNewCheckQuntity(idcate, idtype, lessQu, qunt);
                    }
                    /////////////////////////////////////////////////////////
                    dbsql.AddNewRequsetSupply(idcate, idtype, qunt, price, name, dec, DateTime.Now);//اضافة طلب جديد
                    Refrsh1();
                }
                
            }
        }

        /// <summary>
        /// //
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(((TextBox)sender).Text .Length<=0)
            {
                ((TextBox)sender).Focus();
            }
        }

       
        void Refrsh1()
        {
            getDate1("");
            GetDate2();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Refrsh1();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /////////


    }
}
