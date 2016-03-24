using System;
using System.Collections.Generic;
using ConsoleRunner;

namespace SampleConsoleApp
{
    class EchoOperaion : OperationTemplate
    {
        protected override Argument[] GetArguments()
        {
            return new Argument[]
            {
                Param.SourceString.AsArg<string>().Required(),
                Param.Repetitions.AsArg<int>().WithDefault("1")
            };
        }

        private static class Param
        {
            public const string SourceString = "str";
            public const string Repetitions = "reps";
        }

        protected override void Execute(IConsoleOut console, IDictionary<string, Argument> args)
        {
            var reps = args[Param.Repetitions].ValueAs<int>();
            var s = args[Param.SourceString];

            for(var i = 0; i < reps; i++)
            {
                console.WriteLine(s.ToString());
            }
        }
    }
}