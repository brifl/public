// Copyright Microsoft 2014

using System;

namespace FileSearch
{
    internal struct SearchQuery
    {
        public bool CaseSensitive;
        public string CurrentDir;
        public string[] Extensions;
        public string FileOpenerPath;
        public Action<string> Report;
        public string SearchTerm;
    }
}