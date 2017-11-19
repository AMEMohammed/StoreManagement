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
        /// //////
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
                comboBox4.ValueMember = "رقم";
                comboBox4.DisplayMember = "اسم الموظف";
                comboBox4.DataSource = dbsql.GetAllUser();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// /////////
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
        /// <summary>
        /// ///////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        }/// <summary>
        /// ////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
        /// <summary>
        /// ////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// ////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// ////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            string IDCat ="";
            string IDTyp ="";
            string IDCurrn ="";
            string Name ="";
            string idUser = "";
            if(checkBox1.Checked==false)
            {
                try
                {
                    IDCat = comboBox1.SelectedValue.ToString();
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }

            }
            if(checkBox2.Checked==false)
            {
                try
                {
                    IDTyp = comboBox2.SelectedValue.ToString();
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
            if(checkBox3.Checked==false)
            {
                try
                {
                    IDCurrn = comboBox3.SelectedValue.ToString();
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
            if(checkBox5.Checked==false)
            {
                try { idUser = comboBox4.SelectedValue.ToString(); }
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            if(textBox4.Text.Length>0)
            {
                Name = textBox4.Text;
            }
            this.Cursor = Cursors.WaitCursor;
            if (checkBox4.Checked ==false)
            {
                try
                {
                    dataGridView1.DataSource = dbsql.PrintRequstRPT(IDCat, IDTyp, IDCurrn, Name,idUser);
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }

            }
            else
            {
                try
                {
                    dataGridView1.DataSource = dbsql.PrintRequstRPT(dateTimePicker1.Value.Date, dateTimePicker2.Value, IDCat, IDTyp, IDCurrn, Name,idUser);
                }
              catch(Exception ex) { MessageBox.Show(ex.Message);  }
            }
            this.Cursor = Cursors.Default;
           

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// ////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataTable dt = new DataTable();

                try
                {
                    ////////// اضافة الاعمدة 
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                    dt.Columns.Add("اجمالي الكمية");
                    dt.Columns.Add("اجمالي السعر");
                    dt.Columns.Add("اجمالي الاجمالي");
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    int sumQint = 0;
                    int SumPrice = 0;
                    int SumAll = 0;
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {

                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int idS = Convert.ToInt32(dr[0].ToString());
                        string nmCa = dr[1].ToString();
                        string nmty = dr[2].ToString();
                        int Qun = Convert.ToInt32(dr[3].ToString());
                        sumQint += Qun;
                        int prs = Convert.ToInt32(dr[4].ToString());
                        SumPrice += prs;
                        int totl = Convert.ToInt32(dr[5].ToString());
                        SumAll += totl;
                        string currn = dr[6].ToString();
                        DateTime dd = DateTime.Parse(dr[7].ToString());
                        string namee = dr[8].ToString();
                        string dec = dr[9].ToString();
                        string nameUser = dbsql.GetUserNameBYIdUser(Contrl.UserId);
                        dt.Rows.Add(idS, nmCa, nmty, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, dd.Date.ToShortDateString(), namee, dec,nameUser, string.Format("{0:##,##}",sumQint), string.Format("{0:##,##}",SumPrice), string.Format("{0:##,##}", SumAll));
                        
                    }
                }
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }


                try
                {

                    this.Cursor = Cursors.WaitCursor;
                    frmREPORT frm = new frmREPORT(3, dt);
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                comboBox4.Enabled = false;
            }
            else
            {
                comboBox4.Enabled = true;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void عددالاسطرالمحددةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                MessageBox.Show(dataGridView1.SelectedRows.Count.ToString());
            }
        }

        private void عددجميعالاسطرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                MessageBox.Show(dataGridView1.RowCount.ToString());
            }
        }

        private void اجماليالكميةالمحددةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int count11 = dataGridView1.SelectedRows.Count;
                int sum = 0;
                for(int i=0;i<count11;i++)
                {
                      sum+= Convert .ToInt32(dataGridView1.SelectedRows[i].Cells[3].Value.ToString());
                 
                }
                 MessageBox.Show(sum.ToString());

            }

        }

        private void اجماليالسعرالمحددToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int count11 = dataGridView1.SelectedRows.Count;
                int sum = 0;
                for (int i = 0; i < count11; i++)
                {
                    sum += Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[4].Value.ToString());
                }
                MessageBox.Show(sum.ToString());

            }
        }

        private void اجماليالاجماليالمحددToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int count11 = dataGridView1.SelectedRows.Count;
                int sum = 0;
                for (int i = 0; i < count11; i++)
                {
                    sum += Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[5].Value.ToString());
                }
                MessageBox.Show(sum.ToString());

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = new DataTable();

                try
                {
                    ////////// اضافة الاعمدة 
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                    dt.Columns.Add("اجمالي الكمية");
                    dt.Columns.Add("اجمالي السعر");
                    dt.Columns.Add("اجمالي الاجمالي");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    int sumQint = 0;
                    int SumPrice = 0;
                    int SumAll = 0;
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {

                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int idS = Convert.ToInt32(dr[0].ToString());
                        string nmCa = dr[1].ToString();
                        string nmty = dr[2].ToString();
                        int Qun = Convert.ToInt32(dr[3].ToString());
                        sumQint += Qun;
                        int prs = Convert.ToInt32(dr[4].ToString());
                        SumPrice += prs;
                        int totl = Convert.ToInt32(dr[5].ToString());
                        SumAll += totl;
                        string currn = dr[6].ToString();
                        DateTime dd = DateTime.Parse(dr[7].ToString());
                        string namee = dr[8].ToString();
                        string dec = dr[9].ToString();
                        string nameUser = dbsql.GetUserNameBYIdUser(Contrl.UserId);
                        dt.Rows.Add(idS, nmCa, nmty, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, dd.Date.ToShortDateString(), namee, dec, nameUser, string.Format("{0:##,##}", sumQint), string.Format("{0:##,##}", SumPrice), string.Format("{0:##,##}", SumAll));

                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }


                try
                {

                    this.Cursor = Cursors.WaitCursor;
                    frmREPORT frm = new frmREPORT(3, dt);
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }
    }
}
