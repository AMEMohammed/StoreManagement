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
    public partial class frmlogin : Form
    { DBSQL dbsql = new DBSQL();
        public frmlogin()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0 & textBox2.Text.Length >0)
            {
                frmMain.checkUser = dbsql.CheckUser(textBox1.Text, textBox2.Text);
               if(frmMain.checkUser==true)
                {
                    this.Close();
                }
               else
                {
                   
           
                        frmMain.checkUser = false;
                        MessageBox.Show("دخول خاطى");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    
                }
            }
          
          

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
