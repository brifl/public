﻿namespace FileSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var searcher = new Searcher();
            searcher.ExecuteSearch(args);
        }
    }
}