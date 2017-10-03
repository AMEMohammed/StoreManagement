using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace StoreManagement
{
    public partial class frmsttingImage : Form
    {
        public frmsttingImage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void frmsttingImage_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.colorBackGround;
            textBox1.Text = Properties.Settings.Default.nmserver;
            textBox3.Text = Properties.Settings.Default.nmdatabase;
            textBox2.Text = Properties.Settings.Default.UserSql;
            textBox4.Text = Properties.Settings.Default.PassSql;
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                if (MessageBox.Show("هل تريد اكمال العملة", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes)
                {
                    Properties.Settings.Default.nmserver = textBox1.Text;
                    Properties.Settings.Default.nmdatabase = textBox3.Text;
                    Properties.Settings.Default.UserSql = textBox2.Text;
                    Properties.Settings.Default.PassSql = textBox4.Text;
                    Properties.Settings.Default.Save();
                    this.Close();
                }

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
