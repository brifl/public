using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace TravelingSalesman
{
    internal class Runner
    {
        private readonly Calculator _calculator = new Calculator();
        private readonly GraphGenerator _graphGenerator = new GraphGenerator();
        private readonly GraphRenderer _renderer = new GraphRenderer();
        private readonly Repository _repo;
        private readonly Validator _validator = new Validator();

        public Runner(string workingDir)
        {
            _repo = new Repository(workingDir);
        }

        public void Run(Graph graph, ITspSolver solver, out RunReport report)
        {
            report = new RunReport
            {
                SolverName = solver.GetType().Name,
                Graph = graph
            };

            var timer = new Stopwatch();
            timer.Start();

            var runReport = report;
            solver.TrySolve(graph, attempt => RegisterAttempt(runReport, attempt.ToArray()), runReport.RunDetails);

            timer.Stop();
            report.Duration = timer.Elapsed;
        }

        private void RegisterAttempt(RunReport report, Node[] attempt)
        {
            _validator.Validate(report.Graph.Nodes, attempt);
            var distance = _calculator.Distance(attempt);

            if (report.BestDistance == null || distance < report.BestDistance)
            {
                report.BestWalk = attempt;
                report.BestDistance = distance;
            }
        }

        public IEnumerable<ComparativeRunReport> RunBattery(BruteForce baseSolver, ITspSolver testedSolver,
            int numberOfRuns = 10, int nodesInGraph = 5, int maxX = 10, int maxY = 10,
            SaveType saveType = SaveType.OnFailure, bool stopOnError = false,
            Func<ComparativeRunReport, object> selectiveSave = null, bool includeImages = false)
        {
            var runs = new List<ComparativeRunReport>(numberOfRuns);
            for (int i = 0; i < numberOfRuns; i++)
            {
                var run = SingleComparativeRun(baseSolver, testedSolver, nodesInGraph, maxX, maxY, saveType,
                    selectiveSave, includeImages);
                runs.Add(run);
                if (stopOnError && run.Outcome != ComparativeRunReport.OutcomeText.Success)
                {
                    break;
                }
            }

            return runs;
        }

        public ComparativeRunReport SingleComparativeRun(ITspSolver baseSolver, ITspSolver testedSolver,
            int nodesInGraph = 5, int maxX = 10, int maxY = 10,
            SaveType saveType = SaveType.OnFailure, Func<ComparativeRunReport, object> selectiveSave = null,
            bool includeImages = false)
        {
            var graph = _graphGenerator.Generate(nodesInGraph, maxX, maxY);

            var comparativeReport = CompareWithGraph(graph, baseSolver, testedSolver, saveType, selectiveSave,
                includeImages);
            return comparativeReport;
        }

        public ComparativeRunReport CompareWithGraph(Graph graph, ITspSolver baseSolver, 
            ITspSolver testedSolver, SaveType saveType = SaveType.OnFailure, 
            Func<ComparativeRunReport, object> selectiveSave = null, bool includeImages = false)
        {
            RunReport baseReport = null;
            RunReport testedReport = null;
            Exception exception = null;

            try
            {
                baseReport = null;
                testedReport = null;
                exception = null;

                Run(graph, baseSolver, out baseReport);
                Run(graph, testedSolver, out testedReport);
            }
            catch (Exception e)
            {
                exception = e;
            }

            var comparativeReport = ComparativeRunReport.Create(baseReport, testedReport, exception);
            
            switch (saveType)
            {
                case SaveType.None:
                    break;
                case SaveType.OnFailure:
                    if (comparativeReport.Outcome != ComparativeRunReport.OutcomeText.Success)
                    {
                        Save(comparativeReport, selectiveSave, includeImages);
                    }
                    break;
                case SaveType.All:
                    Save(comparativeReport, selectiveSave, includeImages);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(saveType), saveType, null);
            }

            return comparativeReport;
        }

        private void Save(ComparativeRunReport runReport, Func<ComparativeRunReport, object> selectiveSave = null,
            bool includeImages = false)
        {
            var images = new List<Image>();
            if (includeImages)
            {
                images.Add(_renderer.RenderGraph(runReport.Graph));
                images.Add(_renderer.RenderGraph(runReport.Graph, runReport.BaseSolverReport.BestWalk));
                images.Add(_renderer.RenderGraph(runReport.Graph, runReport.TestedSolverReport.BestWalk));
            }

            selectiveSave = selectiveSave ?? (x => x);
            _repo.Save(selectiveSave(runReport), images.ToArray());
        }
    }

    public enum SaveType
    {
        None,
        OnFailure,
        All
    }
}