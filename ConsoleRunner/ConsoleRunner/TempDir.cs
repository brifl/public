using System;

namespace ConsoleRunner
{
    internal class TempDir : IDisposable
    {
        private readonly FileSystem _fileSystem = new FileSystem();

        public string Path { get; }

        public TempDir()
        {
            Path = _fileSystem.RandomDir(create: true);
        }

        public void Dispose()
        {
            _fileSystem.Delete(Path);
        }

        public override string ToString()
        {
            return Path;
        }
    }
}