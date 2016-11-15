using System;
using System.Collections.Generic;

namespace TravelingSalesman
{
    internal interface ITspSolver
    {
        bool TrySolve(Graph graph, Action<Node[]> registerAttempt, Dictionary<string, object> runDetails);
    }
}