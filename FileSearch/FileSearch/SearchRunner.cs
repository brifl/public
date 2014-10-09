// Copyright Microsoft 2014

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    internal class SearchRunner
    {
        private static async Task AddMatch(string searchTerm, string file, StringComparison comparer, ConcurrentBag<string> foundFiles)
        {
            const int BUFFER_SIZE = 4096;
            using (var stream = File.OpenRead(file))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8, true, BUFFER_SIZE))
                {
                    while (true)
                    {
                        var line = await reader.ReadLineAsync();
                        if (line == null)
                            break;

                        if (line.IndexOf(searchTerm, comparer) >= 0)
                        {
                            foundFiles.Add(file);
                            break;
                        }
                    }
                }
            }
        }

        private static ConcurrentBag<string> FindMatches(SearchQuery searchQuery, IEnumerable<string> filesToSearch)
        {
            var foundFiles = new ConcurrentBag<string>();
            var searchTerm = searchQuery.SearchTerm;
            var comparer = searchQuery.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            var tasksToWait = filesToSearch.Select(file => AddMatch(searchTerm, file, comparer, foundFiles)).ToArray();
            Task.WaitAll(tasksToWait);

            return foundFiles;
        }

        private static List<string> GetFilesToSearch(SearchQuery searchQuery)
        {
            var filesToSearch = new List<string>();

            foreach (var extension in searchQuery.Extensions)
            {
                var files = Directory.GetFiles(searchQuery.CurrentDir, "*." + extension, SearchOption.AllDirectories);
                filesToSearch.AddRange(files);
            }
            return filesToSearch;
        }

        public IEnumerable<string> Run(SearchQuery searchQuery)
        {
            searchQuery.Report(@"Building file list.");
            var filesToSearch = GetFilesToSearch(searchQuery);
            searchQuery.Report(string.Format(@"Searching {0} files.", filesToSearch.Count));

            var foundFiles = FindMatches(searchQuery, filesToSearch);

            foreach (var foundFile in foundFiles)
            {
                searchQuery.Report(foundFile);
            }

            searchQuery.Report(string.Format("Search complete. Found matches in {0} files:", foundFiles.Count));

            return foundFiles;
        }
    }
}