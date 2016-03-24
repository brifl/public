using System;

namespace ConsoleRunner
{
    internal class CommandLineArgumentException : ArgumentException
    {
        public CommandLineArgumentException(string message) : base(message) { }
    }
}