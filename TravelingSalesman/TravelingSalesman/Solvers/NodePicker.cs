using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesman
{
    internal class NodePicker : ITspSolver
    {
        protected readonly Calculator Calculator = new Calculator();
        protected readonly NodeFinderCalc NodeFinder = new NodeFinderCalc();

        public bool TrySolve(Graph graph, Action<Node[]> registerAttempt, Dictionary<string, object> runDetails)
        {
            var edgeHistory = new List<Node[]>();

            var workingSet = new WorkingSet(graph);

            var firstEdge = GetFirstEdge(workingSet);
            workingSet.AddToWalk(firstEdge); //1D line
            workingSet.AddToWalk(firstEdge); //2D line
            edgeHistory.Add(FormatWalk(workingSet.CurrentWalk));

            while (workingSet.NodesNotInGraph.Count > 0)
            {
                var nextNode = GetNextNode(workingSet);
                var edgeToSplit = GetEdgeToSplit(nextNode, workingSet.CurrentWalk);
                workingSet.SplitEdge(nextNode, edgeToSplit);
                edgeHistory.Add(FormatWalk(workingSet.CurrentWalk));
            }

            var walk = FormatWalk(workingSet.CurrentWalk);
            runDetails["history"] = edgeHistory;
            registerAttempt(walk);

            return true;
        }

        private static Node[] FormatWalk(List<EdgeDistance> edges)
        {
            var walk = new List<Node>();
            var copy = new List<EdgeDistance>(edges);
            var first = copy[0];

            walk.Add(first.NodeA);
            walk.Add(first.NodeB);
            
            var currentNode = first.NodeB;
            copy.Remove(first);

            while (copy.Count > 0)
            {
                var nextEdge = copy.FirstOrDefault(x => x.NodeA.Equals(currentNode) || x.NodeB.Equals(currentNode));
                if (nextEdge == null)
                {
                    break;
                }

                if (nextEdge.NodeA.Equals(currentNode))
                {
                    walk.Add(nextEdge.NodeB);
                    currentNode = nextEdge.NodeB;
                }
                else
                {
                    walk.Add(nextEdge.NodeA);
                    currentNode = nextEdge.NodeA;
                }

                copy.Remove(nextEdge);
            }

            walk.RemoveAt(walk.Count - 1);

            var firstNodeName = walk.Min(x => x.Name);

            while (true)
            {
                var node = walk[0];
                if (node.Name == firstNodeName)
                {
                    break;
                }

                walk.Add(node);
                walk.RemoveAt(0);
            }

            return walk.ToArray();
        }

        protected virtual EdgeDistance GetEdgeToSplit(Node node, List<EdgeDistance> edges)
        {
            EdgeDistance toSplit = null;
            var shortestAddedDistance = double.MaxValue;
            foreach (var edge in edges)
            {
                var a = Calculator.Distance(node, edge.NodeA);
                var b = Calculator.Distance(node, edge.NodeB);
                var addedDistance = (a + b) - edge.Distance;
                if (addedDistance < shortestAddedDistance)
                {
                    toSplit = edge;
                    shortestAddedDistance = addedDistance;
                }
            }
            return toSplit;
        }

        protected virtual Node GetNextNode(WorkingSet workingSet)
        {
            var sortedDistancesInGraph = workingSet.Distances.Values;
            NodeDistance candidate = null;
            foreach (var distance in sortedDistancesInGraph)
            {
                var node = distance[0];
                if (candidate == null || (node.Distance > candidate.Distance))
                {
                    candidate = node;
                }
            }
            return candidate.Node;
        }

        protected virtual EdgeDistance GetFirstEdge(WorkingSet workingSet)
        {
            var nodes = workingSet.NodesNotInGraph.ToArray();
            var count = nodes.Length;

            EdgeDistance furthestNodes = null;

            for (int i = 0; i < count; i++)
            {
                var from = nodes[i];
                for (int j = i + 1; j < count; j++)
                {
                    var to = nodes[j];
                    var distance = Calculator.Distance(from, to);
                    if (furthestNodes == null || distance > furthestNodes.Distance)
                    {
                        furthestNodes = new EdgeDistance(from, to, distance);
                    }
                }
            }

            return furthestNodes;
        }

        protected class WorkingSet
        {
            private static readonly Calculator Calculator = new Calculator();

            public readonly List<EdgeDistance> CurrentWalk = new List<EdgeDistance>();
            public readonly Dictionary<Node, List<NodeDistance>> Distances = new Dictionary<Node, List<NodeDistance>>();
            public readonly HashSet<Node> NodesNotInGraph;

            public WorkingSet(Graph graph)
            {
                NodesNotInGraph = new HashSet<Node>(graph.Nodes);
            }

            public void AddToWalk(EdgeDistance edge)
            {
                CurrentWalk.Add(edge);
                MoveNodeToWalk(edge.NodeA);
                MoveNodeToWalk(edge.NodeB);
            }

            private void MoveNodeToWalk(Node node)
            {
                if (NodesNotInGraph.Contains(node))
                {
                    NodesNotInGraph.Remove(node);
                    Distances[node] =
                        NodesNotInGraph.Select(x => new NodeDistance {Node = x, Distance = Calculator.Distance(node, x)})
                            .OrderByDescending(x => x.Distance).ToList();
                    RemoveFromCurrentDistances(node);
                }
            }

            private void RemoveFromCurrentDistances(Node node)
            {
                foreach (var keyValuePair in Distances)
                {
                    var kvpDistances = keyValuePair.Value;
                    for (int i = 0; i < kvpDistances.Count; i++)
                    {
                        var kvpDistance = kvpDistances[i];
                        if (kvpDistance.Node.Equals(node))
                        {
                            kvpDistances.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            public void SplitEdge(Node node, EdgeDistance edgeToSplit)
            {
                CurrentWalk.Remove(edgeToSplit);
                AddToWalk(new EdgeDistance(node, edgeToSplit.NodeA));
                AddToWalk(new EdgeDistance(node, edgeToSplit.NodeB));
            }
        }
    }
}