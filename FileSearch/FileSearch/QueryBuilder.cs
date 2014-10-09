using System;
using System.IO;
using System.Linq;

namespace FileSearch
{
    internal class QueryBuilder
    {
        public bool TryParseArgs(string[] args, out SearchQuery searchQuery)
        {
            searchQuery = new SearchQuery();

            if(args.IsEmpty())
            {
                return false;
            }

            var argLookup = args.ToParamDictionary();

            if(argLookup.ContainsKey("?"))
            {
                return false;
            }

            try
            {
                searchQuery.CurrentDir = Directory.GetCurrentDirectory();
                searchQuery.SearchTerm = argLookup["s"].Single();
                searchQuery.CaseSensitive = argLookup.ContainsKey("c");
                searchQuery.Extensions = argLookup["e"].SelectMany(x => x.Split(',')).Select(x => x.Trim()).ToArray();
                searchQuery.FileOpenerPath = argLookup.ContainsKey("o") ? argLookup["o"].Single() : @"C:\Program Files (x86)\Notepad++\notepad++.exe";
                searchQuery.Report = Console.WriteLine;

                return File.Exists(searchQuery.FileOpenerPath);
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}