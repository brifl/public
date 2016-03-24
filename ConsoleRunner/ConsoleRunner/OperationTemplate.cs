using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleRunner
{
    public abstract class OperationTemplate : IOperation
    {
        public static readonly string ArgToken = "-";
        public static readonly char[] ArgTokenChars = ArgToken.ToCharArray();

        public void Execute(IConsoleOut console, string[] args)
        {
            console.WriteLine("Parsing arguments");
            var allArguments = GetArguments();
            try
            {
                ParseArguments(args, allArguments);
                console.WriteLine("Parsed arguments successfully");
            }
            catch (CommandLineArgumentException ce)
            {
                console.WriteLine("InvalidArguments: " + ce.Message);
                console.WriteLine(Usage());
                return;
            }

            console.WriteLine("Executing program with the following paramters:");
            console.WriteLine(CurrentValues(allArguments));
            try
            {
                var validArguments = allArguments.ToDictionary(x => x.Name);
                Execute(console, validArguments);
                console.WriteLine("Executed successfully");
            }
            catch (Exception)
            {
                console.WriteLine("Execution failed with exception");
                throw;
            }
        }

        private static void ParseArguments(string[] args, IReadOnlyList<Argument> parseIntoArray)
        {
            var tokenEncountered = false;
            var parseIntoIndex = 0;
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                string argName;
                var isArgName = TryGetArgName(arg, out argName);

                if (isArgName)
                {
                    tokenEncountered = true;
                }

                if (tokenEncountered)
                {
                    if (!isArgName)
                    {
                        throw new CommandLineArgumentException($"Expected an argument name, but got '{arg}'");
                    }

                    var parseIntoArg = parseIntoArray.FirstOrDefault(x => x.Name.ToLowerInvariant() == argName);
                    if (parseIntoArg == null)
                    {
                        throw new CommandLineArgumentException($"Argument '{arg}' is not supported");
                    }

                    parseIntoArg.StringValue = args[i + 1].Trim();
                    i++;
                }
                else
                {
                    var parseIntoArg = parseIntoArray[parseIntoIndex];

                    parseIntoArg.StringValue = arg.Trim();

                    parseIntoIndex++;
                }
            }

            if (parseIntoArray.Any(x => !x.IsValid()))
            {
                throw new CommandLineArgumentException($"Argument '{parseIntoArray.First(x => !x.IsValid()).Name}' is not valid.");
            }
        }

        private static bool TryGetArgName(string arg, out string argName)
        {
            arg = arg.Trim();
            if (arg.StartsWith(ArgToken))
            {
                argName = arg.TrimStart(ArgTokenChars).Trim().ToLowerInvariant();
                return true;
            }

            argName = "";
            return false;
        }

        protected abstract Argument[] GetArguments();

        protected virtual string Usage()
        {
            var sb = new StringBuilder();

            sb.AppendLine(
                $"Usage: Pass names and values for the required arguments (e.g. {ArgToken}argName1 value1 {ArgToken}argName2 value2 ...)");

            var arguments = GetArguments();

            foreach (var argument in arguments)
            {
                var argDescription =
                    $"\t{ArgToken}{argument.Name.ToLength(25, true)}|  Type: {argument.Type.ToString().ToLength(30, true)}|  IsRequired: {argument.IsRequired}";
                sb.AppendLine(argDescription);
            }

            return sb.ToString();
        }

        protected string CurrentValues(IEnumerable<Argument> args)
        {
            var sb = new StringBuilder();

            foreach (var argument in args)
            {
                var argDescription =
                    $"\t{ArgToken}{argument.Name.ToLength(25, true)}|  Type:{argument.Type.ToString().ToLength(30, true)}|  Value:{argument.StringValue ?? ""}";
                sb.AppendLine(argDescription);
            }

            return sb.ToString();
        }

        protected abstract void Execute(IConsoleOut console, IDictionary<string, Argument> args);
    }
}