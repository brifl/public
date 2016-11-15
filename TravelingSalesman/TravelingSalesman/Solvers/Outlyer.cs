using System.Linq;

namespace TravelingSalesman
{
    class Outlyer : NodePicker
    {
        protected override Node GetNextNode(WorkingSet workingSet)
        {
            var nodesInGraph = workingSet.Distances.Keys.ToArray();

            var candidates = workingSet.NodesNotInGraph.ToList();
            

            Node bestNode = null;
            double bestValue = default(double);
            
            foreach (var candidate in candidates)
            {
                var attempt = nodesInGraph.Select(x => Calculator.Distance(candidate, x)).Sum();
                if (attempt > bestValue)
                {
                    bestNode = candidate;
                    bestValue = attempt;
                }
            }

            return bestNode;
        }
    }
}