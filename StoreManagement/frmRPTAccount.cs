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
    public partial class frmRPTAccount : Form
    {
        DBSQL dbsql = new DBSQL();
        public frmRPTAccount()
        {
            InitializeComponent();
        }
        /// <summary>
        /// /////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            string idcat = "";
            string idtyp = "";
            string idcurr = "";
       
            {
                if (checkBox1.Checked == false)
                {
                    try
                    {
                        idcat = comboBox1.SelectedValue.ToString();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
                if (checkBox2.Checked == false)
                {
                    try { idtyp = comboBox2.SelectedValue.ToString(); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
                if (checkBox3.Checked == false)
                {
                    try { idcurr = comboBox3.SelectedValue.ToString(); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                try { dataGridView1.DataSource = dbsql.PrintAccountQuntity(idcat, idtyp, idcurr); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        /// <summary>
        /// ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
            }
        }
        /// <summary>
        /// //////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// //////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// //////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRPTAccount_Load(object sender, EventArgs e)
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
                GetData1();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// //
        /// </summary>
        void GetData1()
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ///
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
                }
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }

                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {

                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        string nmca = dr[0].ToString();
                        string nmty = dr[1].ToString();
                        int qunt = Convert.ToInt32(dr[2].ToString());
                        int pres = Convert.ToInt32(dr[3].ToString());
                        string currnt = dr[4].ToString();
                        dt.Rows.Add(nmca, nmty, string.Format("{0:##,##}", qunt), string.Format("{0:##,##}", pres), currnt);
                    }
                }catch(Exception ex) { MessageBox.Show(ex.Message); }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmREPORT frm = new frmREPORT(5, dt);
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
