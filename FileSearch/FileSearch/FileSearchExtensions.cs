using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSearch
{
    internal static class FileSearchExtensions
    {
        public static IDictionary<string, IList<string>> ToParamDictionary(this string[] args)
        {
            var argLookup = new Dictionary<string, IList<string>>(StringComparer.InvariantCultureIgnoreCase);

            var currentKey = "s";
            argLookup[currentKey] = new List<string>();

            foreach(var a in args)
            {
                var arg = a.Trim();
                if(arg.StartsWith("-"))
                {
                    currentKey = arg.Trim().Substring(1);
                    argLookup[currentKey] = new List<string>();
                }
                else
                {
                    argLookup[currentKey].Add(arg);
                }
            }

            return argLookup;
        }

        public static bool IsEmpty(this string[] args)
        {
            return args == null || !args.Any();
        }
    }
}