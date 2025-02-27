namespace PathFinding
{
    public class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.AddNode("A");
            graph.AddNode("B");
            graph.AddNode("C");
            graph.AddNode("D");
            graph.AddNode("E");
            graph.AddNode("F");

            graph.LinkEdge("A", "B", 2);
            graph.LinkEdge("A", "C", 4);
            graph.LinkEdge("B", "D", 8);
            graph.LinkEdge("C", "D", 3);
            graph.LinkEdge("C", "E", 7);
            graph.LinkEdge("D", "E", 2);
            graph.LinkEdge("D", "F", 6);
            graph.LinkEdge("E", "F", 3);
            graph.FindPath("A", "F");
        }
    }
}