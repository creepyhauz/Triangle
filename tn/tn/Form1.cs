using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tn
{
    public partial class Form1 : Form
    {
        Nodes nodes = new Nodes();
        bool removeNode;
        string args;
        public Form1(string[] a)
        {
            InitializeComponent();
            button4.Visible = false;
            if(a.Length>0)
            {
                nodes.nodes=load(a[0]);
                h = nodes.tc();
                pictureBox1.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nodes.addNode();
        }

        Random r = new Random();
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < h.Length; i++)
            {
                Point[] k = new Point[3];
                if (h[i] != null)
                {
                    k[0] = new Point(h[i][0].pos.X + h[i][0].size.Width / 2, h[i][0].pos.Y + h[i][0].size.Height / 2);
                    k[1] = new Point(h[i][1].pos.X + h[i][1].size.Width / 2, h[i][1].pos.Y + h[i][1].size.Height / 2);
                    k[2] = new Point(h[i][2].pos.X + h[i][2].size.Width / 2, h[i][2].pos.Y + h[i][2].size.Height / 2);
                    //  e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))), k);
                    if (checkBox1.Checked) e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(100, 255, 100)), k);
                }
            }
            if (radioButton4.Checked) nodes.drawAll(e.Graphics, debug);
            if (radioButton5.Checked) nodes.drawNode(e.Graphics, debug);
            if (radioButton6.Checked) nodes.drawLinks(e.Graphics, debug);
        }

        Node fn;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (nodes.md(e.Location) != null)
            {
                if (radioButton2.Checked||radioButton3.Checked&&!removeNode)
                {
                    fn = nodes.md(e.Location);

                }

                if(removeNode)
                {
                    nodes.removeNode(nodes.gnbp(e.Location));
                    removeNode = false;
                    h = nodes.tc();
                    pictureBox1.Invalidate();
                }

                
            }
        }
        Node[][] h = new Node[0][];
        Node n;
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            n = nodes.mu(e.Location);
            if (n != null && fn != null && radioButton2.Checked&&!fn.Equals(n))
            {
                fn.addLink(n);
                n.addLink(fn);
            }
            if (n != null && fn != null && radioButton3.Checked)
            {
              // MessageBox.Show(fn.name + " " + n.name);
                fn.removeLink(n);
                n.removeLink(fn);
                for (int i = 0; i < h.Length; i++)
                {
                    if (h[i] != null)
                    {

                      //  MessageBox.Show("НЕ НУЛЬ");
                        if (h[i][0].Equals(n) || h[i][0].Equals(fn) || h[i][1].Equals(n) || h[i][1].Equals(fn) || h[i][2].Equals(n) || h[i][2].Equals(fn))
                        {
                            h = nodes.tc();
                        }
                    }
                }
            }

            if (radioButton2.Checked && nodes.tc() != null)
            {

                h = nodes.tc();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked&&!removeNode)
            {
                nodes.mm(e.Location);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            button4.Visible = removeNode;
            groupBox1.Enabled = !removeNode;
            groupBox2.Enabled = !removeNode;
            button1.Enabled = !removeNode;
            button3.Enabled = !removeNode;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            nodes.mu(new Point(0, 0));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            removeNode = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            removeNode = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            button1.Enabled = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(nodes.nodes);
            MessageBox.Show("W.I.P");
            f2.ShowDialog();
        }
        bool debug = false;
        private void dEBUGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debug = !debug;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            nodes.removeAllNode();
            h = nodes.tc();
        }
        public void save(string str, Node[] g)
        {
            FileStream f;
            f = new FileStream(str, FileMode.Create, FileAccess.Write);
            BinaryFormatter p = new BinaryFormatter();
            p.Serialize(f, g);
            f.Close();
        }
        public Node[] load(string path)
        {
            Node[] m=null;
            FileStream F = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter p2 = new BinaryFormatter();
            if (F.Position < F.Length)
            {
                m= (Node[])p2.Deserialize(F);
            }
            F.Close();
            return m;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                save(saveFileDialog1.FileName, nodes.nodes);
            }
        }
       public bool mode = false;
        private void button7_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                mc m = new mc(this);
                m.ShowDialog();
                if (mode)
                {
                    Node[] f=load(openFileDialog1.FileName);
                    for(int i=0;i<f.Length;i++)
                    {
                        nodes.addNode(f[i]);
                    }
                    h = nodes.tc();
                }
                else
                {
                    nodes.nodes= load(openFileDialog1.FileName);
                    h = nodes.tc();
                }
            }
        }
    }
}
