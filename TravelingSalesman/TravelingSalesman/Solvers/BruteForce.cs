using System;
using System.Collections.Generic;

namespace TravelingSalesman
{
    internal class BruteForce : ITspSolver
    {
        public bool TrySolve(Graph graph, Action<Node[]> registerAttempt, Dictionary<string, object> runDetails)
        {
            var allPermutations = new TspPermuationWalker<Node>(graph.Nodes);
            foreach (var permutation in allPermutations)
            {
                registerAttempt(permutation);
            }
            return true;
        }
    }
}