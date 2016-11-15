using System.Linq;

namespace TravelingSalesman
{
    class ExplodeImplode : NodePicker
    {
        private bool _isExploding = true;

        protected override EdgeDistance GetFirstEdge(WorkingSet workingSet)
        {
            _isExploding = true;
            return base.GetFirstEdge(workingSet);
        }

        protected override Node GetNextNode(WorkingSet workingSet)
        {
            var edges = workingSet.CurrentWalk.Cast<Edge>().ToList();
            
            if (_isExploding)
            {
                var nodes = workingSet.NodesNotInGraph.ToList();
                var insideNodes = NodeFinder.GetInsideNodes(edges, nodes);
                var outsideNodes = workingSet.NodesNotInGraph.Except(insideNodes).ToList();
                _isExploding = outsideNodes.Any();

                if (_isExploding)
                {
                    return NodeFinder.FurthestNearestEdge(edges, outsideNodes).Node;
                }
            }
            return GetNextInnerNode(workingSet);
        }

        private Node GetNextInnerNode(WorkingSet workingSet)
        {
            return base.GetNextNode(workingSet);
        }
    }
}