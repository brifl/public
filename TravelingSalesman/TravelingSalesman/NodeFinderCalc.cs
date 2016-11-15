using System;
using System.Collections.Generic;

namespace TravelingSalesman
{
    class NodeFinderCalc
    {
        private static readonly Calculator Calculator = new Calculator();

        public NodeEdgePair LargestAreaFromEdges(IEnumerable<Edge> edges, IEnumerable<Node> nodes)
        {
            return BestNodeFromEdges(edges, nodes, (e, n) => Calculator.Area(e.NodeA, e.NodeB, n),
                (att, best) => att > best);
        }

        public NodeEdgePair FurthestAnyEdge(IEnumerable<Edge> edges, IEnumerable<Node> nodes)
        {
            return BestNodeFromEdges(edges, nodes,
                (e, n) => Calculator.Distance(n, e.NodeA) + Calculator.Distance(n, e.NodeB),
                (att, best) => att > best);
        }

        public NodeEdgePair FurthestNearestEdge(IEnumerable<Edge> edges, IEnumerable<Node> nodes)
        {
            Node bestNode = null;
            Edge bestEdge = null;
            var bestDistance = 0.0;

            foreach (var node in nodes)
            {
                //Func<Edge, Node, double> distanceCalc = (e, n) => Calculator.Area(e.NodeA, e.NodeB, n);
                Func<Edge, Node, double> distanceCalc = (e, n) => Calculator.AddedDistance(e, n);
                double distance;
                var nearest = BestEdgeFromNode(edges, node,
                    distanceCalc,
                    (att, best) => att < best, out distance);
                
                if (bestNode == null || distance > bestDistance)
                {
                    bestNode = node;
                    bestEdge = nearest;
                    bestDistance = distance;
                }
            }

            return new NodeEdgePair
            {
                Node = bestNode,
                Edge = bestEdge
            };
        }

        public NodeEdgePair ClosestToAnyEdge(IEnumerable<Edge> edges, IEnumerable<Node> nodes)
        {
            return BestNodeFromEdges(edges, nodes,
                (e, n) => Calculator.Distance(n, e.NodeA) + Calculator.Distance(n, e.NodeB),
                (att, best) => att < best);
        }

        public NodeEdgePair ShortestCFromEdges(IEnumerable<Edge> edges, IEnumerable<Node> nodes)
        {
            return BestNodeFromEdges(edges, nodes, (e, n) => Calculator.LengthC(e.NodeA, e.NodeB, n),
                (att, best) => att < best);
        }

        public NodeEdgePair BestNodeFromEdges<T>(IEnumerable<Edge> edges, IEnumerable<Node> nodes, Func<Edge, Node, T> getValue,
            Func<T, T, bool> compareAttemptWithBest)
        {
            Node bestNode = null;
            Edge bestEdge = null;
            T bestValue = default(T);

            foreach (var edge in edges)
            {
                foreach (var node in nodes)
                {
                    var attempt = getValue(edge, node);
                    if (bestNode == null || compareAttemptWithBest(attempt, bestValue))
                    {
                        bestNode = node;
                        bestEdge = edge;
                        bestValue = attempt;
                    }
                }
            }

            return new NodeEdgePair
            {
                Node = bestNode,
                Edge = bestEdge
            };
        }

        public Edge BestEdgeFromNode<T>(IEnumerable<Edge> edges, Node node, Func<Edge, Node, T> getValue,
            Func<T, T, bool> compareAttemptWithBest, out T bestValue)
        {
            Edge bestEdge = null;
            bestValue = default(T);

            foreach (var edge in edges)
            {
                var attempt = getValue(edge, node);
                if (bestEdge == null || compareAttemptWithBest(attempt, bestValue))
                {
                    bestEdge = edge;
                    bestValue = attempt;
                }
            }

            return bestEdge;
        }

        public List<Node> GetInsideNodes(List<Edge> edges, IEnumerable<Node> nodes)
        {
            var inside = new List<Node>();

            foreach (var node in nodes)
            {
                if (IsInside(node, edges))
                {
                    inside.Add(node);
                }
            }

            return inside;
        }

        public bool IsInside(Node node, List<Edge> edges)
        {
            var intersectionCount = 0;
            var topBorderCount = 0;
            var bottomBorderCount = 0;

            foreach (var edge in edges)
            {
                var intersectionType = Calculator.GetIntersectionType(node, edge);
                if (intersectionType == IntersectionType.Intersecting || intersectionType == IntersectionType.LineBorder)
                {
                    intersectionCount++;
                }

                if (intersectionType == IntersectionType.BottomBorder)
                {
                    bottomBorderCount++;
                }

                if (intersectionType == IntersectionType.TopBorder)
                {
                    topBorderCount++;
                }
            }

            var bottomBorderOdd = bottomBorderCount%2 == 1;
            var topBorderOdd = topBorderCount%2 == 1;
            if (topBorderOdd || bottomBorderOdd)
            {
                intersectionCount++;
            }

            var isEvenCrossing = intersectionCount%2 == 0;

            return !isEvenCrossing;
        }

        public NodeEdgePair LeastAddedDistance(List<Edge> edges, List<Node> nodes)
        {
            return BestNodeFromEdges(edges, nodes, Calculator.AddedDistance, (att, best) => att < best);
        }
    }
}