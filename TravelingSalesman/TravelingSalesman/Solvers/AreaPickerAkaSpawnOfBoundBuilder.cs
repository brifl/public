using System.Linq;

namespace TravelingSalesman
{
    class AreaPickerAkaSpawnOfBoundBuilder : NodePicker
    {
        protected override Node GetNextNode(WorkingSet workingSet)
        {
            var edges = workingSet.CurrentWalk;
            var candidates = workingSet.NodesNotInGraph.ToList();
            return NodeFinder.LargestAreaFromEdges(edges, candidates).Node;
        }
    }
}