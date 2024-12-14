using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    public class Node
    {
        public enum EType
        {
            And, Or, Not, Single
        }
        public string term;
        public Node[] children = null;
        public Node parent = null;
        public EType type;
        public Division dtype = null;

        public Node(Node _parent, string _term, Division _dtype)
        {
            parent = _parent;
            term = _term;
            dtype = _dtype;
            this.type = EType.Single;
            children = new Node[0];
        }
        public bool hasChildren()
        {
            if (children != null)
                return true;
            return false;
        }
        public bool isRoot()
        {
            if (parent == null)
                return true;
            return false;
        }
        public void addChild(Node nChild)
        {
            ArrayList al = null;

            if (children != null)
                al = new ArrayList(children);
            else
                al = new ArrayList();

            al.Add(nChild);
            children = (Node[])al.ToArray(typeof(Node));
        }
    }
}
