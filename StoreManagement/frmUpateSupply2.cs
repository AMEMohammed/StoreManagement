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
    public partial class frmUpateSupply2 : Form
    {
        public frmUpateSupply2()
        {
            InitializeComponent();
        }
        public frmUpateSupply2(DataTable dt)
        {
            InitializeComponent();
            dataGridView1.DataSource = dt;
        }

        private void frmUpateSupply2_Load(object sender, EventArgs e)
        {

        }
    }
}
