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
        { try
            {
                openFileDialog1.Filter = "JPEG|*.jpg|Bitmaps|*.bmp|GIFS|*.gif";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.BorderStyle = BorderStyle.Fixed3D;
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                    MessageBox.Show(openFileDialog1.SafeFileName);
                }
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DBSQL dbsql = new DBSQL();
            if (pictureBox1.Image!=null)
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] bimg = ms.GetBuffer();
              
                dbsql.SaveImg(bimg);
                this.Close();
            }
            DataTable dt = new DataTable();
            dt = dbsql.GetImg();
            byte[] b = ((byte[])dt.Rows[0][0]);
            pictureBox1.Image = Image.FromStream(new MemoryStream(b));
        }
    }
}
