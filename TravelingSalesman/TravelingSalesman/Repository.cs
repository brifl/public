using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TravelingSalesman
{
    internal class Repository
    {
        private readonly FileSystem _fileSystem = new FileSystem();
        private readonly Serializer _serializer = new Serializer();

        public Repository(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
            _fileSystem.EnsureDirectory(workingDirectory);
        }

        public string WorkingDirectory { get; }

        public string Save(object obj, params Image[] images)
        {
            var fileName = GetFileName(obj.GetType());
            var path = PathFor(fileName);

            var serialized = _serializer.Serialize(obj);
            _fileSystem.SaveText($"{path}.json", serialized);

            for (int i = 0; i < images.Length; i++)
            {
                _fileSystem.SaveImage($"{path}.image{i}.bmp", images[i]);
            }
            return path;
        }

        private string PathFor(string file)
        {
            return Path.Combine(WorkingDirectory, file);
        }

        private static string GetFileName(Type type)
        {
            var typename = type.Name;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd+HH.mm.ss");
            var random = Path.GetRandomFileName().Split('.')[0];
            var fileName = $"[{timestamp}][{random}]{typename}";

            return fileName;
        }

        public static void SaveImage(Edge[] edges, Node[] points)
        {
            const string dir = @"C:\Temp\TSP\Renderer";
            var fileName = GetFileName(typeof(Graph));
            var filePath = $"{dir}\\{fileName}.bmp";
            var image = new GraphRenderer().RenderPolygonAndPoints(edges, points);
            new FileSystem().SaveImage(filePath, image);
        }
    }
}