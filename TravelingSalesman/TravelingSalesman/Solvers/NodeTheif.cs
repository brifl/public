using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesman
{
    class NodeTheif : OutsideFirst
    {

        private readonly List<Node> StealableNodes = new List<Node>();

        protected override void NotifyChange(WorkingSet workingSet)
        {
            var edgeA = workingSet.CurrentWalk[workingSet.CurrentWalk.Count - 1];
            var edgeB = workingSet.CurrentWalk[workingSet.CurrentWalk.Count - 2];

            StealBetterNodes(edgeA, edgeB, workingSet, StealableNodes);
        }

        private void StealBetterNodes(Edge edgeA, Edge edgeB, WorkingSet workingSet, List<Node> stealableNodes)
        {
            var newConnection = new NodeConnection(edgeA, edgeB);

            var nodesThatShouldBeStolen = new List<StealCandidate>();
            foreach (var stealableNode in stealableNodes)
            {
                if (Equals(stealableNode, newConnection.Center))
                {
                    continue;
                }

                var candidate = GetCandidate(stealableNode, workingSet.CurrentWalk);
                var removeEdgeSavings = (candidate.AtoCenterDistance + candidate.BtoCenterDistance) - candidate.AtoBDistance;

                var stealingEdgeCandidates = GetEdgesNotContainingNode(candidate.Center, newConnection.EdgeA,
                    newConnection.EdgeB);

                if (stealingEdgeCandidates.Any())
                {
                    double additionalEdgeCost;
                    var stealingEdge = NodeFinder.BestEdgeFromNode(stealingEdgeCandidates, candidate.Center,
                        Calculator.AddedDistance, (att, best) => att < best, out additionalEdgeCost);

                    if (additionalEdgeCost < removeEdgeSavings) //Steal
                    {
                        nodesThatShouldBeStolen.Add(new StealCandidate(stealingEdge, candidate,
                            removeEdgeSavings - additionalEdgeCost));
                    }
                }
            }

            var bestCandidate = nodesThatShouldBeStolen.OrderByDescending(x => x.CostSavings).FirstOrDefault();
            StealNode(bestCandidate, workingSet);
        }

        private IEnumerable<Edge> GetEdgesNotContainingNode(Node shouldntContain, params Edge[] edges)
        {
            return edges.Where(x => !(Equals(x.NodeA, shouldntContain) || Equals(x.NodeB, shouldntContain)));
        }

        private void StealNode(StealCandidate candidate, WorkingSet workingSet)
        {
            if (candidate == null)
            {
                return;
            }

            var walk = workingSet.CurrentWalk;
            var toBeStolen = candidate.ToBeStolen;
            
            walk.Remove(toBeStolen.EdgeA);
            walk.Remove(toBeStolen.EdgeB);
            walk.Add(new Edge(candidate.ToBeStolen.AtoCenter, candidate.ToBeStolen.BtoCenter));
            workingSet.SplitEdge(candidate.ToBeStolen.Center, candidate.StealingEdge);

            AddToHistory(workingSet);
            NotifyChange(workingSet);
        }

        private class StealCandidate
        {
            public Edge StealingEdge { get; }
            public NodeConnection ToBeStolen { get; }
            public double CostSavings { get; }

            public StealCandidate(Edge stealingEdge, NodeConnection toBeStolen, double costSavings)
            {
                StealingEdge = stealingEdge;
                ToBeStolen = toBeStolen;
                CostSavings = costSavings;
            }
        }

        private NodeConnection GetCandidate(Node stealableNode, List<Edge> currentWalk)
        {
            var edgesWithNode = currentWalk.Where(x => x.NodeA.Equals(stealableNode) || x.NodeB.Equals(stealableNode)).ToArray();
            if (edgesWithNode.Length != 2)
            {
                throw new ApplicationException("Only 2 edges should be found with node");
            }

            return new NodeConnection(edgesWithNode[0], edgesWithNode[1]);
        }
        

        private class NodeConnection
        {
            private static readonly Calculator Calc = new Calculator();

            public NodeConnection(Edge edgeA, Edge edgeB)
            {
                EdgeA = edgeA;
                EdgeB = edgeB;

                Center = GetCenter(EdgeA, EdgeB);
                AtoCenter = GetOtherNode(Center, EdgeA);
                BtoCenter = GetOtherNode(Center, EdgeB);
                AtoCenterDistance = Calc.Distance(Center, AtoCenter);
                BtoCenterDistance = Calc.Distance(Center, BtoCenter);
                AtoBDistance = Calc.Distance(AtoCenter, BtoCenter);
            }

            private static Node GetOtherNode(Node node, Edge edge)
            {
                if (node.Equals(edge.NodeA))
                {
                    return edge.NodeB;
                }
                return edge.NodeA;
            }

            private static Node GetCenter(Edge edgeA, Edge edgeB)
            {
                var aa = edgeA.NodeA;
                var ab = edgeA.NodeB;
                var ba = edgeB.NodeA;
                var bb = edgeB.NodeB;

                if (Equals(aa, ba) || Equals(aa, bb))
                {
                    return aa;
                }

                if (Equals(ab, ba) || Equals(ab, bb))
                {
                    return ab;
                }

                throw new ApplicationException("Node is not shared for connection");
            }

            public Node AtoCenter { get; private set; }
            public Node Center { get; private set; }
            public Node BtoCenter { get; private set; }
            public Edge EdgeA { get; private set; }
            public Edge EdgeB { get; private set; }

            public double AtoCenterDistance { get; private set; }
            public double BtoCenterDistance { get; private set; }
            public double AtoBDistance { get; private set; }
        }
        
        protected override void Reset()
        {
            StealableNodes.Clear();
            base.Reset();
        }

        protected override NodeEdgePair GetNextInnerNode(WorkingSet workingSet, List<Node> insideNodes)
        {
            var next = base.GetNextInnerNode(workingSet, insideNodes);
            StealableNodes.Add(next.Node);
            return next;
        }
    }
}