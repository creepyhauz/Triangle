using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tn
{
    
    public partial class mc : Form
    {
        Form1 mf;
        public mc(Form1 form1)
        {
            InitializeComponent();
            mf = form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mf.mode = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mf.mode = false;
            this.Close();
        }
    }
}
