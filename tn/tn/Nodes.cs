using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//for(int i=0;i<nodes.Length;i++)
//{

//}

namespace tn
{
    [Serializable]
    class Nodes
    {
        public Node[] nodes;
        public Nodes()
        {
            nodes = new Node[0];
        }
        public Nodes(int count)
        {
            nodes = new Node[count];
        }
        public Nodes(Node[] n)
        {
            nodes = n;
        }

        public Node md(Point p)
        {
            Node n = null;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    n = nodes[i].md(p);
                    if (n != null)
                    {
                        break;
                    }
                }
            }
            return n;
        }

        public Node gnbp(Point p)
        {
            Node n = null;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    n = nodes[i].gnbp(p);
                    if (n != null)
                    {
                        break;
                    }
                }
            }
            return n;
        }
        public void mm(Point p)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].mm(p);
                }
            }
        }

        public Node mu(Point p)
        {
            Node n = null;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    n = nodes[i].mu(p);
                    if (n != null)
                    {
                        break;
                    }
                }
            }
            return n;
        }

        public void drawNode(Graphics g,bool b)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].drawNode(g,b);
                }
            }
        }

        public void drawLinks(Graphics g,bool b)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].drawLinks(g,b);
                }
            }
        }
        public void drawAll(Graphics g,bool b)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].drawNode(g,b);
                    nodes[i].drawLinks(g,b);
                }

            }
        }

        public void removeNode(Node n)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                if(nodes[i]!=null)
                {
                    if (nodes[i].Links != null)
                    {
                        for (int i2 = 0; i2 < nodes[i].Links.Length; i2++)
                        {
                            if (nodes[i].Links[i2].Links != null)
                            {
                                for (int i3 = 0; i3 < nodes[i].Links[i2].Links.Length; i3++)
                                    if (nodes[i].Links[i2].Links[i3] != null && nodes[i].Links[i2].Links[i3].Equals(n))
                                    {
                                        nodes[i].Links[i2].Links[i3] = null;
                                    }
                            }
                        }
                    }
                }
                if (nodes[i]!=null&&nodes[i].Equals(n))
                {
                    
                    nodes[i] = null;
                    break;
                }
            }
        }

        public void removeAllNode()
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    for (int i2 = 0; i2 < nodes[i].Links.Length; i2++)
                    {
                        if (nodes[i].Links[i2] != null)
                        {
                            for (int i3 = 0; i3 < nodes[i].Links[i2].Links.Length; i3++)
                                if (nodes[i].Links[i2].Links[i3] != null)
                                {
                                    nodes[i].Links[i2].Links[i3] = null;
                                }
                        }
                    }
                }
                if (nodes[i] != null)
                {

                    nodes[i] = null;
                }
            }
        }

        public void addNode(Node n)
        {
            Array.Resize(ref nodes, nodes.Length + 1);
            nodes[nodes.Length - 1] = n;
        }

        public Node[][] tc()
        {
            Node[][] g = new Node[0][];
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    for (int i2 = 0; i2 < nodes[i].Links.Length; i2++)
                    {
                        if (nodes[i].Links[i2] != null)
                        {
                            for (int i3 = 0; i3 < nodes[i].Links[i2].Links.Length; i3++)
                            {
                                if (nodes[i].Links[i2].Links[i3] != null)
                                {
                                    for (int i4 = 0; i4 < nodes[i].Links[i2].Links[i3].Links.Length; i4++)
                                    {
                                        if (nodes[i].Links[i2].Links[i3].Links[i4] != null)
                                        {
                                            if (nodes[i].Links[i2].Links[i3].Links[i4].Equals(nodes[i]))
                                            {
                                                Array.Resize(ref g, g.Length + 1);
                                                g[g.Length - 1] = new Node[] { nodes[i], nodes[i].Links[i2], nodes[i].Links[i2].Links[i3] };
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return g;
        }


        public void addNode()
        {
            bool nm = true;
            int id = 0;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] == null)
                {
                    nm = false;
                    id = i;
                }
            }
            if (nm)
            {
                Array.Resize(ref nodes, nodes.Length + 1);
                nodes[nodes.Length - 1] = new Node(new Point(0, 0), new Size(20, 20), "node:" + nodes.Length);
            }
            else
            {
                nodes[id] = new Node(new Point(0, 0), new Size(20, 20), "node:" + (id+1));
            }
        }
    }
}
