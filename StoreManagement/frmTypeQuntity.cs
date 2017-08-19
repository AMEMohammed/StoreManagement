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
    public partial class frmTypeQuntity : Form
    {
        DBSQL dbsql = new DBSQL();
        public frmTypeQuntity()
        {
            InitializeComponent();
        }

        private void frmTypeQuntity_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dbsql.GetAllTypeQuntity();
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();
            changeLanguage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                dbsql.AddNewTypeQuntity(textBox1.Text);
                Refersh1();
            }
        }
        int id = 0;
        string name = "";
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
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = name;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                name = textBox2.Text;
                dbsql.UpdateTypeQuntity(id, name);
                Refersh1();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("عند حذف (" + name + ")  سيتم حذف جميع المحفوظات المتربطه به هل تريد الاستمرار", "حذف نوع", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes)
            {
                dbsql.DeleteQuntity(id);
                Refersh1();
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
            dataGridView1.DataSource = dbsql.GetAllTypeQuntity();
            textBox1.Text = "";
            textBox2.Text = "";

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
            { }
        }

       
    }
}
