using System.Drawing;
using System.IO;

namespace TravelingSalesman
{
    internal class FileSystem
    {
        public void SaveText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public void SaveImage(string path, Image image)
        {
            image.Save(path);
        }

        public string LoadText(string path)
        {
            return File.ReadAllText(path);
        }

        public void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void ClearDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                var files = Directory.EnumerateFiles(path);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
        }
    }
}