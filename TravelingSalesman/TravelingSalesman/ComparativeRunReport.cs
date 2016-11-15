using System;

namespace TravelingSalesman
{
    internal class ComparativeRunReport
    {
        public RunReport BaseSolverReport;
        public Graph Graph;
        public string Outcome;
        public RunReport TestedSolverReport;
        public double Variance;

        public static ComparativeRunReport Create(RunReport baseSolverReport, RunReport testedSolverReport,
            Exception e = null)
        {
            var report = new ComparativeRunReport();
            report.Graph = baseSolverReport.Graph;
            report.BaseSolverReport = baseSolverReport;
            report.TestedSolverReport = testedSolverReport;
            if (e == null)
            {
                report.Variance = testedSolverReport.BestDistance.Value - baseSolverReport.BestDistance.Value;
                report.Outcome = (report.Variance < 0.0001) ? OutcomeText.Success : OutcomeText.Failure;
            }
            else
            {
                report.Outcome = String.Format(OutcomeText.Exception, e.Message);
            }

            return report;
        }

        public static class OutcomeText
        {
            public const string Success = "Success";
            public const string Failure = "Failure";
            public const string Exception = "Exception: {0}";
        }
    }
}