using System;
using System.Collections.Generic;

namespace TravelingSalesman
{
    internal class GraphGenerator
    {
        private static readonly string[] NodeName =
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
            "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };

        private static readonly Random Randy = new Random();

        public Graph Generate(int nodeCount = 10, int x = 100, int y = 100)
        {
            var nodes = new HashSet<Node>();

            for (int i = 0; i < nodeCount; i++)
            {
                nodes.Add(CreateNode(x, y, i, nodes));
            }

            return new Graph(nodes, x, y);
        }

        private static Node CreateNode(int xMax, int yMax, int i, HashSet<Node> existingNodes)
        {
            while (true)
            {
                var name = GetNodeName(i);
                var x = Randy.Next(xMax);
                var y = Randy.Next(yMax);
                var node = new Node
                {
                    Name = name,
                    X = x,
                    Y = y
                };

                if (existingNodes.Contains(node))
                {
                    continue;
                }
                return node;
            }
        }

        private static string GetNodeName(int count)
        {
            var len = NodeName.Length;
            var name = "";
            var levels = (count/len) + 1;
            for (int i = 0; i < levels; i++)
            {
                name += NodeName[count - (len*i)];
            }
            return name;
        }
    }
}