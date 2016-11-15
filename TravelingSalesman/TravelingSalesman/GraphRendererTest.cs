using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TravelingSalesman
{
    [TestClass]
    public class GraphRendererTest
    {
        [TestMethod]
        public void Render()
        {
            var path = @"C:\Temp\TSP\Renderer";
            var graph = new GraphGenerator().Generate(5, 10, 10);
            var runner = new Runner(path);
            RunReport report;
            runner.Run(graph, new BruteForce(), out report);

            var renderer = new GraphRenderer();
            var image = renderer.RenderGraph(report.Graph, report.BestWalk);

            var repo = new Repository(path);
            repo.Save(report, image);
        }
    }
}
