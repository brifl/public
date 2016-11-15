using System.Collections.Generic;

namespace TravelingSalesman
{
    public class Graph
    {
        public Graph()
        {
            //for deserialization
        }

        internal Graph(HashSet<Node> nodes, int maxX, int maxY)
        {
            Nodes = nodes;
            MaxX = maxX;
            MaxY = maxY;
        }

        public HashSet<Node> Nodes { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
    }
}