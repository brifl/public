using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesman
{
    internal class RunReport
    {
        private const string REPORT_FORMAT = @"***TSP REPORT***
Solver: {0}
Graph node count: {1}
Nodes: [{2}]
Best walk: [{3}]
Best distance: {4}
Duration(ms): {5}";
        public string SolverName { get; set; }
        public Graph Graph { get; set; }
        public TimeSpan Duration { get; set; }
        public Node[] BestWalk { get; set; }
        public double? BestDistance { get; set; }

        public readonly Dictionary<string, object> RunDetails = new Dictionary<string, object>(); 

        public override string ToString()
        {
            var report = string.Format(REPORT_FORMAT,
                SolverName,
                Graph.Nodes.Count,
                FullNodeDetails(Graph.Nodes),
                string.Join("-", BestWalk.Select(x => x.Name)),
                BestDistance,
                Duration.TotalMilliseconds);

            return report;
        }

        private static string FullNodeDetails(IEnumerable<Node> nodes)
        {
            var nodeStrings = nodes.Select(x => $"'{x.Name}' X:{x.X} Y:{x.Y}");
            return string.Join(",", nodeStrings);
        }
    }
}