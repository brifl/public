// Copyright Microsoft 2014

using System;

namespace FileSearch
{
    internal class Searcher
    {
        private readonly FileOpener _opener;
        private readonly QueryBuilder _queryBuilder;
        private readonly SearchRunner _runner;
        public Searcher() : this(new QueryBuilder(), new SearchRunner(), new FileOpener()) {}

        private Searcher(QueryBuilder queryBuilder, SearchRunner runner, FileOpener opener)
        {
            _queryBuilder = queryBuilder;
            _runner = runner;
            _opener = opener;
        }

        public void ExecuteSearch(string[] args)
        {
            SearchQuery searchQuery;
            if (_queryBuilder.TryParseArgs(args, out searchQuery))
            {
                var results = _runner.Run(searchQuery);
                _opener.ConfirmOpen(results, searchQuery);
            }
            else
            {
                Console.WriteLine(
                    @"Usage: FileSearch.exe -s <search term> -e <comma delimited extensions> [-o] <file opener path> [-c] (if case sensitive)");
            }
        }
    }
}