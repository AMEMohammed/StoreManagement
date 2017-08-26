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
    public partial class frmPlaceSend : Form
    {
        DBSQL dbsql = new DBSQL(); 
        public frmPlaceSend()
        {
            InitializeComponent();
        }

        private void frmPlaceSend_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;
            try
            {
                dataGridView1.DataSource = dbsql.GetAllPlace();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
                changeLanguage();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        int id = 0;
        string name = "";
        /// <summary>
        /// //
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                try
                {
                    dbsql.AddNewPlaceSend(textBox1.Text);
                    Refersh1();
                    textBox1.Focus();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                try
                {
                    name = textBox2.Text;
                    dbsql.UpdatePlaceSend(id, name);
                    Refersh1();
                }
                catch ( Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("عند حذف (" + name + ")  سيتم حذف جميع المحفوظات المتربطه به هل تريد الاستمرار", "حذف جهة", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes)
            {
                try
                {
                    dbsql.DeletePlaceSend(id);
                    Refersh1();
                }
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    textBox2.Text = name;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Refersh1();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// 
        public void Refersh1()
        {
            try
            {
                dataGridView1.DataSource = dbsql.GetAllPlace();
                textBox1.Text = "";
                textBox2.Text = "";
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void changeLanguage()
        {
            foreach (InputLanguage lng in InputLanguage.InstalledInputLanguages)
            {
                if (lng.LayoutName == "العربية (101)")
                    InputLanguage.CurrentInputLanguage = lng;
            }
        }
    }
}
