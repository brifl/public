namespace TravelingSalesman
{
    internal class Edge
    {
        public Edge(){ }

        public Edge(Node a, Node b)
        {
            NodeA = a;
            NodeB = b;
        }

        public Node NodeA;
        public Node NodeB;
    }
}