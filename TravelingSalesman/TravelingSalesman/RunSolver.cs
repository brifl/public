using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TravelingSalesman
{
    [TestClass]
    public class RunSolver
    {
        private static readonly string FirstFailurePath = @"C:\Temp\TSP\FirstFailure";
        private static readonly string FirstExceptionPath = @"C:\Temp\TSP\FirstException";
        private static readonly string StatisticalOutputPath = @"C:\Temp\TSP\OutputDump";
        private readonly FileSystem _fileSystem = new FileSystem();
        private readonly Serializer _serializer = new Serializer();
        private readonly GraphGenerator _graphGenerator = new GraphGenerator();
        private readonly Runner _runner = new Runner(StatisticalOutputPath);

        [TestMethod]
        public void StatisticalTest()
        {
            var runs =
                _runner.RunBattery(new BruteForce(), new NodeTheif(), 100, 9, 8, 8, SaveType.None, false, null,
                    true)
                    .ToArray();

            Console.WriteLine($"details saved at '{StatisticalOutputPath}'");

            var nonErrored = runs.Where(x => !x.Outcome.StartsWith("Exception")).ToArray();

            Console.WriteLine($"{runs.Length - nonErrored.Length} of {runs.Length} ended with exception");

            var baseSum = nonErrored.Select(x => x.BaseSolverReport.BestDistance.Value).Sum();
            var testedSum = nonErrored.Select(x => x.TestedSolverReport.BestDistance.Value).Sum();

            var averageVariancePercent = Math.Round(((testedSum/baseSum) - 1.0)*100.0, 3, MidpointRounding.ToEven);

            Console.WriteLine($"avg variance: {averageVariancePercent}%");
            Console.WriteLine(
                $"{runs.Count(x => x.Outcome.Equals(ComparativeRunReport.OutcomeText.Success))} of {runs.Length} found the optimal path");

            SaveFirstFailure(runs);
            SaveFirstException(runs);

            Assert.IsTrue(runs.All(x => x.Outcome.Equals(ComparativeRunReport.OutcomeText.Success)),
                "Not all runs were successful");
        }

        private void SaveFirstException(ComparativeRunReport[] runs)
        {
            var exception = runs.FirstOrDefault(x => x.Outcome.StartsWith("Exception"));
            if (exception != null)
            {
                var serialized = _serializer.Serialize(exception.Graph);
                _fileSystem.SaveText(FirstExceptionPath + "\\Graph.json", serialized);
            }
        }

        [TestMethod]
        public void TryAgain()
        {
            var graphText = _fileSystem.LoadText(FirstFailurePath + @"\Graph.json");
            var graph = _serializer.Deserialize<Graph>(graphText);

            var report = _runner.CompareWithGraph(graph, new BruteForce(), new NodeTheif(), includeImages: true);

            SaveSingleRun(report, FirstFailurePath);

            Assert.IsTrue(report.Outcome.Equals(ComparativeRunReport.OutcomeText.Success));
        }

        [TestMethod]
        public void TryExceptionAgain() //BoundAndGagged is best
        {
            var graphText = _fileSystem.LoadText(FirstExceptionPath + @"\Graph.json");
            var graph = _serializer.Deserialize<Graph>(graphText);

            var report = _runner.CompareWithGraph(graph, new BruteForce(), new NodeTheif());

            SaveSingleRun(report, FirstExceptionPath);

            Assert.IsFalse(report.Outcome.StartsWith("Exception"));
        }

        private void SaveFirstFailure(ComparativeRunReport[] runs)
        {
            var firstFailure = runs.FirstOrDefault(x => x.Outcome.Equals(ComparativeRunReport.OutcomeText.Failure));
            if (firstFailure != null)
            {
                SaveSingleRun(firstFailure, FirstFailurePath);
            }
        }

        private void SaveSingleRun(ComparativeRunReport run, string rootPath)
        {
            _fileSystem.EnsureDirectory(FirstFailurePath);
            _fileSystem.ClearDirectory(FirstFailurePath);
            var filePath = rootPath + @"\{0}.bmp";

            var renderer = new GraphRenderer();

            _fileSystem.SaveText(rootPath + @"\Graph.json", _serializer.Serialize(run.Graph));

            _fileSystem.SaveImage(string.Format(filePath, "best"),
                renderer.RenderGraph(run.Graph, run.BaseSolverReport.BestWalk));

            var history = (List<Node[]>) run.TestedSolverReport.RunDetails["history"];
            var index = 1;
            foreach (var walk in history)
            {
                _fileSystem.SaveImage(string.Format(filePath, "step" + index++),
                    renderer.RenderGraph(run.Graph, walk));
            }
        }

        private RunReport RunWithGraph(Graph graph, ITspSolver solver)
        {
            RunReport report;
            _runner.Run(graph, solver, out report);
            Console.WriteLine(report);
            return report;
        }

        [TestMethod]
        public void BoundBuilder()
        {
            var graph = _graphGenerator.Generate(11, 20, 20);
            RunWithGraph(graph, new NodePicker());
        }

        [TestMethod]
        public void BruteForce()
        {
            var graph = _graphGenerator.Generate(11, 20, 20);
            RunWithGraph(graph, new BruteForce());
        }
    }
}