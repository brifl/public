using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesman
{
    internal class OutsideFirst : ITspSolver
    {
        protected readonly Calculator Calculator = new Calculator();
        protected readonly NodeFinderCalc NodeFinder = new NodeFinderCalc();
        private List<Node[]> _edgeHistory = new List<Node[]>();
        
        public bool TrySolve(Graph graph, Action<Node[]> registerAttempt, Dictionary<string, object> runDetails)
        {
            Reset();
            _edgeHistory = new List<Node[]>();

            var workingSet = new WorkingSet(graph);

            try
            {
                var firstEdge = GetFirstEdge(workingSet);
                workingSet.AddToWalk(firstEdge); //1D line
                workingSet.AddToWalk(firstEdge); //2D line
                AddToHistory(workingSet);

                while (workingSet.NodesNotInGraph.Count > 0)
                {
                    var nextNode = GetNextNode(workingSet);
                    var edgeToSplit = GetEdgeToSplit(workingSet.CurrentWalk, nextNode);
                    workingSet.SplitEdge(nextNode.Node, edgeToSplit);
                    AddToHistory(workingSet);
                    NotifyChange(workingSet);
                }

                var walk = FormatWalk(workingSet.CurrentWalk);
                registerAttempt(walk);
            }
            finally
            {
                runDetails["history"] = _edgeHistory;
            }

            return true;
        }

        protected void AddToHistory(WorkingSet workingSet)
        {
            _edgeHistory.Add(FormatWalk(workingSet.CurrentWalk));
        }
        
        protected virtual void NotifyChange(WorkingSet workingSet)
        {
        }

        protected virtual void Reset()
        {
        }

        private static Node[] FormatWalk(List<Edge> edges)
        {
            var walk = new List<Node>();
            var copy = new List<Edge>(edges);
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

        private Edge GetFirstEdge(WorkingSet workingSet)
        {
            var nodes = workingSet.NodesNotInGraph.ToArray();
            var count = nodes.Length;

            Edge furthestNodes = null;
            var furthestNodesDistance = 0.0;
            for (int i = 0; i < count; i++)
            {
                var from = nodes[i];
                for (int j = i + 1; j < count; j++)
                {
                    var to = nodes[j];
                    var distance = Calculator.Distance(from, to);
                    if (furthestNodes == null || distance > furthestNodesDistance)
                    {
                        furthestNodes = new Edge(from, to);
                        furthestNodesDistance = distance;
                    }
                }
            }

            return furthestNodes;
        }
        
        private NodeEdgePair GetNextNode(WorkingSet workingSet)
        {
            var insideNodes = NodeFinder.GetInsideNodes(workingSet.CurrentWalk, workingSet.NodesNotInGraph);
            var outsideNodes = workingSet.NodesNotInGraph.Except(insideNodes).ToList();
            var hasOuterNodes = outsideNodes.Any(); 
            if (hasOuterNodes)
            {
                return GetNextOuterNode(workingSet, outsideNodes);
            }

            return GetNextInnerNode(workingSet, insideNodes);
        }

        protected virtual NodeEdgePair GetNextOuterNode(WorkingSet workingSet, List<Node> outsideNodes)
        {
            var furthestNearest = NodeFinder.FurthestNearestEdge(workingSet.CurrentWalk, outsideNodes);
            return furthestNearest;
        }

        protected virtual NodeEdgePair GetNextInnerNode(WorkingSet workingSet, List<Node> insideNodes)
        {
            return NodeFinder.LeastAddedDistance(workingSet.CurrentWalk, insideNodes);
        }

        protected Edge GetEdgeToSplit(List<Edge> edges, NodeEdgePair nodeEdge)
        {
            return nodeEdge.Edge;
        }

        protected class WorkingSet
        {
            public readonly string GraphJson;

            public readonly List<Edge> CurrentWalk = new List<Edge>();
            public readonly HashSet<Node> NodesNotInGraph;

            public WorkingSet(Graph graph)
            {
                GraphJson = new Serializer().Serialize(graph);
                NodesNotInGraph = new HashSet<Node>(graph.Nodes);
            }

            public void AddToWalk(Edge edge)
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
                }
            }

            public void SplitEdge(Node node, Edge edgeToSplit)
            {
                CurrentWalk.Remove(edgeToSplit);
                var newEdges = Tuple.Create(new Edge(node, edgeToSplit.NodeA), new Edge(node, edgeToSplit.NodeB));
                AddToWalk(newEdges.Item1);
                AddToWalk(newEdges.Item2);
            }
        }
    }
}