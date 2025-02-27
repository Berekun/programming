using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Node
    {
        private string _name;
        private List<Edge> _edges = new List<Edge>();

        public string Name => _name;
        public List<Edge> Edges => _edges;

        public Node(string name)
        {
            _name = name;
        }
        public void AddEdge(Node node, int value)
        {
            _edges.Add(new Edge(value, node));
        }
        public void RemoveEdge(Edge edge)
        {
            _edges.Remove(edge);
        }



    }
}
