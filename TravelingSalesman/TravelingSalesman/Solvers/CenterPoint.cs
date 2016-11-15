using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesman
{
    class CenterPoint : NodePicker
    {
        private Queue<Node> _fromCenterPoint = null;
        protected override Node GetNextNode(WorkingSet workingSet)
        {
            return _fromCenterPoint.Dequeue();
        }

        protected override EdgeDistance GetFirstEdge(WorkingSet workingSet)
        {
            var allNodes = workingSet.NodesNotInGraph;

            var centerPoint = GetCenterPoint(allNodes);
            _fromCenterPoint = new Queue<Node>(
                allNodes.Select(x => new NodeDistance { Node = x, Distance = Calculator.Distance(centerPoint, x) })
                    .OrderByDescending(x => x.Distance).Select(x => x.Node));

            return new EdgeDistance(_fromCenterPoint.Dequeue(), _fromCenterPoint.Dequeue());
        }

        protected Coordinate GetCenterPoint(IEnumerable<Node> nodes)
        {
            var center = new Coordinate
            {
                X = nodes.Average(node => node.X),
                Y = nodes.Average(node => node.Y)
            };

            return center;
        }
    }
    class AdaptiveCenterPoint : CenterPoint
    {
        protected override Node GetNextNode(WorkingSet workingSet)
        {
            return GetFurthestFromCenter(workingSet.NodesNotInGraph);
        }

        protected override EdgeDistance GetFirstEdge(WorkingSet workingSet)
        {
            var nodes = workingSet.NodesNotInGraph;

            var centerPoint = GetCenterPoint(nodes);
            var furthestFrom =
                nodes.Select(x => new NodeDistance {Node = x, Distance = Calculator.Distance(centerPoint, x)})
                    .OrderByDescending(x => x.Distance)
                    .Select(x => x.Node)
                    .ToList();
            return new EdgeDistance(furthestFrom[0], furthestFrom[1]);
        }

        private Node GetFurthestFromCenter(IEnumerable<Node> nodes)
        {
            var centerPoint = GetCenterPoint(nodes);
            var furthestFrom = nodes.Select(x => new NodeDistance { Node = x, Distance = Calculator.Distance(centerPoint, x) })
                    .OrderByDescending(x => x.Distance).Select(x => x.Node).First();

            return furthestFrom;
        }
    }
}