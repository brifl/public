using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesman
{
    class BoundAndGagged : NodePicker
    {
        protected override Node GetNextNode(WorkingSet workingSet)
        {
            var edges = workingSet.CurrentWalk;
            var candidates = workingSet.NodesNotInGraph.ToList();
            
            var nearestEdges = new List<EdgeDistance>();

            foreach (var candidate in candidates)
            {
                var edgeDistances = new List<EdgeDistance>();
                foreach (var edge in edges)
                {
                    var distance = Calculator.LengthC(edge.NodeA, edge.NodeB, candidate);
                    edgeDistances.Add(new EdgeDistance
                    {
                        Node = candidate,
                        Distance = distance,
                        Edge = edge
                    });
                }
                nearestEdges.Add(Nearest(edgeDistances));
            }

            var furthestNearest = Furthest(nearestEdges);

            return furthestNearest.Node;
        }

        private EdgeDistance Nearest(List<EdgeDistance> edgeDistances)
        {
            EdgeDistance nearest = null;

            foreach (var edgeDistance in edgeDistances)
            {
                if (nearest == null || edgeDistance.Distance < nearest.Distance)
                {
                    nearest = edgeDistance;
                }
            }
            return nearest;
        }

        private EdgeDistance Furthest(List<EdgeDistance> edgeDistances)
        {
            EdgeDistance furthest = null;

            foreach (var edgeDistance in edgeDistances)
            {
                if (furthest == null || edgeDistance.Distance > furthest.Distance)
                {
                    furthest = edgeDistance;
                }
            }

            return furthest;
        }
        
        class EdgeDistance
        {
            public TravelingSalesman.EdgeDistance Edge;
            public Node Node;
            public double Distance;
        }
    }
}