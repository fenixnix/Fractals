using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Map
{
    public class NPathNode
    {
        NLocate loc = new NLocate();
        NPathNode parent = null;
        public List<NPathNode> Childs = new List<NPathNode>();
        public int RouteCost = 10;
        public int TotelCost = 10;

        public NPathNode Parent { get { return parent; }}
        public NLocate Locate { get { return loc; } }

        public NPathNode()
        {

        }

        public NPathNode(int x, int y, NPathNode parent = null)
        {
            loc = new NLocate(x, y);
            this.parent = parent;
        }

        public bool IsRoot
        {
            get { return Parent == null; }
        }

        public NPathNode AddChild(int x, int y)
        {
            var cNode = new NPathNode(x, y, this);
            cNode.TotelCost = cNode.parent.TotelCost + cNode.RouteCost;
            Childs.Add(cNode);

            return cNode;
        }

        public IEnumerable<NLocate> GetPath()
        {
            NPathNode curNode = this;
            if (curNode.IsRoot)
            {
                yield break;
            }
            do
            {
                yield return curNode.Locate;
                curNode = curNode.Parent;
            } while (!curNode.IsRoot);
            //NPathNode curNode = this;
            //while (!curNode.IsRoot) ;
            //{
            //    yield return curNode.Locate;
            //    curNode = curNode.Parent;
            //} 

            yield break;
        }
    }
}
