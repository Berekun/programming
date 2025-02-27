using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Graph
    {
        private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();

        public void AddNode(string name)
        {
            _nodes.Add(name, new Node(name));
        }

        public void LinkEdge(string nodeInitial, string nodeFinal, int value)
        {
            _nodes[nodeInitial].AddEdge(_nodes[nodeFinal], value);
        }

        public List<Node> FindPath(string nodeInitial, string nodeFinal)
        {
            Dictionary<Node, int> distance = new Dictionary<Node, int>();
            Dictionary<Node, Node> nodePrev = new Dictionary<Node, Node>();

            List<Node> nodeUnvisited = new List<Node>();

            Node nodeStart = _nodes[nodeInitial];
            Node nodeFin = _nodes[nodeFinal];

            foreach (Node node in _nodes.Values)
            {
                if (node == nodeStart)
                    distance[node] = 0;
                else
                    distance[node] = int.MaxValue;

                nodePrev[node] = null;

                nodeUnvisited.Add(node);
            }

            while (nodeUnvisited.Count > 0)
            {
                Node nodeActual = GetNodeSmallDistance(distance, nodeUnvisited);

                if (nodeActual == nodeFin)
                    return CreatePath(nodePrev, nodeActual);

                nodeUnvisited.Remove(nodeActual);

                foreach (Edge edge in nodeActual.Edges)
                {
                    int dist = distance[nodeActual] + edge.Value;

                    if (dist < distance[edge.NodeAdy])
                    {
                        distance[edge.NodeAdy] = dist;
                        nodePrev[edge.NodeAdy] = nodeActual;
                    }

                }               
            }

            return null;
        }
        public Node GetNodeSmallDistance(Dictionary<Node, int> distances, List<Node> nodes)
        {
            Node smallestNode = null;
            int smallestDistance = int.MaxValue;

            foreach (Node node in nodes)
            {
                if (distances[node] < smallestDistance)
                {
                    smallestNode = node;
                    smallestDistance = distances[node];
                }
            }

            return smallestNode;
        }

        public List<Node> CreatePath(Dictionary<Node, Node> nodePrev, Node nodeFin)
        {
            List<Node> nodePath = new List<Node>();

            Node node = nodeFin;

            while (node != null)
            {
                nodePath.Add(node);
                node = nodePrev[node];
            }

            nodePath.Reverse();
            return nodePath;
        }
    }
}
