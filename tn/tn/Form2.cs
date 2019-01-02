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
    public partial class Form2 : Form
    {
        Node[] f;
        public Form2(Node[] n)
        {
            InitializeComponent();
            f = n;
            if (n.Length > 0)
            {
                dataGridView1.RowCount = n.Length;
            }
            else
            {
                dataGridView1.RowCount = 1;
            }
            dataGridView1.ColumnCount = 4;

            for(int i=0;i<n.Length;i++)
            {
                if (n[i] != null)
                {
                    dataGridView1.Rows[i].Cells[0].Value = n[i].name;
                    dataGridView1.Rows[i].Cells[1].Value = n[i].pos;
                    dataGridView1.Rows[i].Cells[2].Value = n[i].size;
                    dataGridView1.Rows[i].Cells[3].Value = n[i].Links.Length;
                    
                }
                else
                {
                    dataGridView1.Rows[i].Cells[0].Value = "empty";
                    dataGridView1.Rows[i].Cells[1].Value = "empty";
                    dataGridView1.Rows[i].Cells[2].Value = "empty";
                    dataGridView1.Rows[i].Cells[3].Value = 0;
                }
            }
        }



        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==3)
            {
                if (f != null&&f[e.RowIndex]!=null)
                {
                    Form2 f2 = new Form2(f[e.RowIndex].Links);
                    f2.ShowDialog();
                }
            }
        }
    }
}
