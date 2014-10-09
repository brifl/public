// Copyright Microsoft 2014

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileSearch
{
    internal class FileOpener
    {
        public void ConfirmOpen(IEnumerable<string> results, SearchQuery searchQuery)
        {
            searchQuery.Report(string.Format(@"Open files with ""{0}""? Enter 'Y' or 'N'.", searchQuery.FileOpenerPath));
            var choice = Console.ReadLine() ?? "";

            if (choice.ToUpper() == "Y")
            {
                var opener = searchQuery.FileOpenerPath;
                foreach (var file in results)
                {
                    Process.Start(opener, file);
                }
            }
        }
    }
}