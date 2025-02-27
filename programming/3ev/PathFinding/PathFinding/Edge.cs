using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Edge
    {
        private int _value;

        private Node _nodeAdy;
        public int Value => _value;
        public Node NodeAdy => _nodeAdy;
        public Edge(int value, Node nodeAdy)
        {
            _value = value;
            _nodeAdy = nodeAdy;
        }
    }
}
