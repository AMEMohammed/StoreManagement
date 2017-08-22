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
        {   try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        
        }


        void getDate1( )
        {
            comboBox1.DisplayMember = "اسم الصنف";
            comboBox1.ValueMember = "رقم الصنف";
            comboBox1.DataSource = dbsql.GetAllCategoryAR() ;
            comboBox2.DisplayMember = "اسم النوع";
            comboBox2.ValueMember = "رقم النوع";
            comboBox2.DataSource = dbsql.GetAllTypeQuntity();
            comboBox3.DisplayMember = "اسم العملة";
            comboBox3.ValueMember = "رقم العملة";
            comboBox3.DataSource = dbsql.GetAllCurrency();
            dataGridView1.DataSource = dbsql.SearchINRequsetSupplyDate(DateTime.Now.Date, DateTime.Now);
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
            if(textBox1.Text.Length>0&&textBox2.Text.Length>0  && (int)comboBox1.SelectedValue>0&& (int)comboBox2.SelectedValue > 0 )
            { if ((MessageBox.Show("هل تريد ترحيل طلب التوريد واعتماده ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {
                    int idcate = (int)comboBox1.SelectedValue;
                 
                    int idtype = (int)comboBox2.SelectedValue;
                    int idCurrnt = (int)comboBox3.SelectedValue;
                    int qunt = Convert.ToInt32(textBox1.Text);
                    int price = Convert.ToInt32(textBox2.Text);
                 
                    string name = textBox4.Text;
                    string dec = textBox5.Text;
                    int idAcount = dbsql.CheckAccountIsHere(idcate, idtype, price,idCurrnt);
                  
                
                    if (idAcount > 0) // في حالة الحساب موجود من قبل
                    {   //  تعديل الحساب بالكمية الجديدة
                        int oldQunt = dbsql.GetQuntityInAccount(idAcount);
                        int newQunt = oldQunt + qunt;
                        dbsql.UpdateQuntityAccount(idAcount, newQunt);


                    }
                    else //  في حالة الحساب جديد
                    {
                        dbsql.AddNewAccount(idcate,idtype,qunt,price,idCurrnt);// اضافة حساب جديد
                    }
                    /////////////////////////////////
                
                    /////////////////////////////////////////////////////////
                    dbsql.AddNewRequsetSupply(idcate, idtype, qunt, price,idCurrnt, name, dec, DateTime.Now);//اضافة طلب جديد
                    if ((MessageBox.Show("هل تريد طباعة سند توريد؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                    {
                        try
                        {
                            int IDRequstSupply = dbsql.GetMaxSupplyid();
                            frmREPORT frmr = new frmREPORT();
                            frmr.Tag = IDRequstSupply;
                            frmr.ShowDialog();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }
                    }

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
            getDate1();
     
            textBox1.Text = "";
            textBox2.Text = "";
         
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //

        private void button1_Click(object sender, EventArgs e)
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
        /////////


    }
}
