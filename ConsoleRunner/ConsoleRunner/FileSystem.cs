using System.IO;

namespace ConsoleRunner
{
    internal class FileSystem
    {
        public string CurrentDir => Directory.GetCurrentDirectory();

        public string RandomDir(string root = null, bool create = false)
        {
            root = root ?? Path.GetTempPath();
            var dir = Path.Combine(root, Path.GetRandomFileName());

            if (create)
            {
                Directory.CreateDirectory(dir);
            }

            return dir;
        }

        public string PathConcat(params string[] pathParts)
        {
            return Path.Combine(pathParts);
        }

        public string FullPath(string path)
        {
            return Path.GetFullPath(path);
        }

        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }

        public void Delete(string path)
        {
            if (Directory.Exists(path))
            {
                // Directory.Delete(path, true);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void CreateDir(string path, bool deleteExistingDirectory = false)
        {
            if (deleteExistingDirectory)
            {
                Delete(path);
            }

            Directory.CreateDirectory(path);
        }

        public void SaveFile(string path, string content)
        {
            var dir = Path.GetDirectoryName(path);
            CreateDir(dir);
            File.WriteAllText(path, content);
        }

        public string[] ListFiles(string dir, string searchPattern = "*")
        {
            return Directory.GetFiles(dir, searchPattern, SearchOption.AllDirectories);
        }

        public string[] ListDirs(string dir)
        {
            return Directory.GetDirectories(dir);
        }

        public string GetDirectoryName(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            var lastIndex = dir.LastIndexOf(Path.DirectorySeparatorChar);
            return dir.Substring(lastIndex + 1);
        }

        public string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }
    }
}