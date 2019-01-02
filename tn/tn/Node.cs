using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Vector;

namespace tn
{
    [Serializable]
    public class Node
    {
        public Point pos;
        public string name;
        public Size size;
        public Node[] Links =new Node[0];
        public Point LastPos;
        bool clicked  = false;

        public Node(Point p, Size s, string n)
        {
            pos = p;
            size = s;
            name = n;
        }

        public void drawNode(Graphics g,bool b)
        {
            g.DrawRectangle(Pens.Black,new Rectangle(pos,size));
            g.DrawString(name, new Font("aria",7),Brushes.Red,pos);
        }
        public void drawNode(Graphics g)
        {
            g.DrawRectangle(Pens.Black, new Rectangle(pos, size));
            g.DrawString(name, new Font("aria", 7), Brushes.Red, pos);
        }
        public void drawLinks(Graphics g)
        {
            for (int i = 0; i < Links.Length; i++)
            {
                if (Links[i] != null)
                {
                    g.DrawString(name+"->"+Links[i].name,new Font("arial",8),Brushes.Blue, new vector2(Links[i].pos.X + Links[i].size.Width / 2, Links[i].pos.Y + Links[i].size.Height / 2).ToPoint);
                    g.DrawLine(Pens.Red, new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2), new Point(Links[i].pos.X + Links[i].size.Width / 2, Links[i].pos.Y + Links[i].size.Height / 2));
                }
            }
        }

        public void drawLinks(Graphics g,bool b)
        {
            for (int i = 0; i < Links.Length; i++)
            {
                if (Links[i] != null)
                {
                    if(b)g.DrawString(name + "->" + Links[i].name, new Font("arial", 8), Brushes.Blue, new vector2(Links[i].pos.X + Links[i].size.Width / 2, (Links[i].pos.Y + Links[i].size.Height / 2) + i * 12).ToPoint);
                    g.DrawLine(Pens.Red, new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2), new Point(Links[i].pos.X + Links[i].size.Width / 2, Links[i].pos.Y + Links[i].size.Height / 2));
                }
            }
        }

        public void removeLink( Node n)
        {
            for (int i = 0; i < Links.Length; i++)
            {
                if (Links[i]!=null&&Links[i].Equals(n))
                {
                    Links[i] = null;
                    break;
                }
            }
        }

        public Node md(Point p)
        {
            if(p.X>pos.X&&p.X<pos.X+size.Width&&p.Y>pos.Y&&p.Y<pos.Y+size.Width)
            {
                clicked = true;
                LastPos = new Point(p.X - pos.X, p.Y - pos.Y);
                return this;
            }
            else
            {
                return null;
            }
        }

        public Node gnbp(Point p)
        {
            if (p.X > pos.X && p.X < pos.X + size.Width && p.Y > pos.Y && p.Y < pos.Y + size.Width)
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public void mm(Point p)
        {
 
            if(clicked)
            {
                pos = new Point(p.X-LastPos.X,p.Y-LastPos.Y);
            }
            
        }

        public Node mu(Point p)
        {
            clicked = false;
            if (p.X > pos.X && p.X < pos.X + size.Width && p.Y > pos.Y && p.Y < pos.Y + size.Width)
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public void addLink(Node n)
        {
            bool nm = true;
            int id = 0;
            for(int i=0;i<Links.Length;i++)
            {
                if(Links[i]==null)
                {
                    nm = false;
                    id = i;
                    break;
                }
            }
            if (nm)
            {
                Array.Resize(ref Links, Links.Length + 1);
                Links[Links.Length - 1] = n;
            }
            else
            {
                Links[id] = n;
            }
        }
    }
}
