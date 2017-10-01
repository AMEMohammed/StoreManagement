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
    public partial class frmUser : Form
    {
        DBSQL dbsql = new DBSQL();
        int idus = 0;
        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();
            label1.BackColor = Properties.Settings.Default.colorBackGround;
            try
            {

                dataGridView1.DataSource = dbsql.GetAllUser();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                if ((MessageBox.Show("هل تريد اضافة مستخدم جديد ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {
                    try
                    {

                        dbsql.AddNewUser(textBox1.Text, textBox2.Text, textBox3.Text, checkBox1.Checked, checkBox2.Checked, checkBox4.Checked, checkBox3.Checked, checkBox5.Checked, checkBox6.Checked);
                        refrish1();
                    }
                    catch(Exception ex)

                                        {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        void refrish1()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            idus = 0;
            dataGridView1.DataSource = dbsql.GetAllUser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && idus>0)
            {
                if ((MessageBox.Show("هل تعديل بيانات المستخدم ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {
                    try {
                        dbsql.UpdUsers(idus, textBox1.Text, textBox2.Text, textBox3.Text, checkBox1.Checked, checkBox2.Checked, checkBox4.Checked, checkBox3.Checked, checkBox5.Checked, checkBox6.Checked);
                        refrish1();
                    }
                    catch(Exception ex)
                    { MessageBox.Show(ex.Message); }

                }
            }


                }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                try
                {
                    idus = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    checkBox1.Checked = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    checkBox2.Checked = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                    checkBox3.Checked = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                    checkBox4.Checked = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
                    checkBox5.Checked = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[9].Value.ToString());
                    checkBox6.Checked = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[8].Value.ToString());
                }
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }


            }
        }
    }
}
