using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TravelingSalesman
{
    internal class GraphRenderer
    {
        private const int PixelsPerSquare = 40;
        private const int EdgeBuffer = 10;

        public Bitmap RenderGraph(Graph graph, Node[] walk = null)
        {
            var bitmap = GetInitialBitmap(graph.MaxX, graph.MaxX);
            var gfx = Graphics.FromImage(bitmap);
            Fill(gfx, bitmap);
            AddGrid(gfx, graph.MaxX, graph.MaxX);
            AddNodes(gfx, graph.Nodes, graph.MaxY);
            AddEdges(gfx, walk, graph.MaxY);

            return bitmap;
        }

        public Bitmap RenderPolygonAndPoints(Edge[] edges, Node[] points)
        {
            var allNodes = edges.Select(x => x.NodeA).Concat(edges.Select(x => x.NodeB)).Concat(points).ToArray();
            var maxX = allNodes.Max(x => x.X);
            var maxY = allNodes.Max(x => x.Y);

            var bitmap = GetInitialBitmap(maxX, maxY);
            var gfx = Graphics.FromImage(bitmap);
            Fill(gfx, bitmap);
            AddGrid(gfx, maxX, maxY);
            AddNodes(gfx, allNodes, maxY);
            AddEdges(gfx, edges, maxY);

            return bitmap;
        }

        private void Fill(Graphics gfx, Bitmap bitmap)
        {
            gfx.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
        }

        private void AddEdges(Graphics gfx, Node[] edges, int maxY)
        {
            if (edges == null || edges.Length <= 1)
            {
                return;
            }

            var path = new GraphicsPath();

            for (int i = 0; i < edges.Length; i++)
            {
                var from = edges[i];

                var to = (i == edges.Length - 1) ? edges[0] : edges[i + 1];

                path.AddLine(GridPoint(from.X), GridPoint(maxY-from.Y), GridPoint(to.X), GridPoint(maxY-to.Y));
            }

            gfx.DrawPath(Pens.Black, path);
        }

        private void AddEdges(Graphics gfx, Edge[] edges, int maxY)
        {
            if (edges == null)
            {
                return;
            }

            var path = new GraphicsPath();

            for (int i = 0; i < edges.Length; i++)
            {
                var from = edges[i].NodeA;

                var to = edges[i].NodeB;

                path.AddLine(GridPoint(from.X), GridPoint(maxY-from.Y), GridPoint(to.X), GridPoint(maxY-to.Y));
            }

            gfx.DrawPath(Pens.Black, path);
        }

        private void AddNodes(Graphics gfx, IEnumerable<Node> nodes, int maxY)
        {
            foreach (var node in nodes)
            {
                AddPoint(gfx, node.X, maxY-node.Y);
            }
        }

        private void AddPoint(Graphics gfx, int x, int y)
        {
            const int size = PixelsPerSquare/4;
            var xStart = GridPoint(x) - (size/2);
            var yStart = GridPoint(y) - (size/2);
            var bounds = new Rectangle(xStart, yStart, size, size);
            gfx.DrawEllipse(Pens.Black, bounds);
            gfx.FillEllipse(Brushes.Black, bounds);
        }

        private int GridPoint(int xy)
        {
            return EdgeBuffer + (xy*PixelsPerSquare);
        }

        private void AddGrid(Graphics gfx, int hSquares, int vSquares)
        {
            var currentX = EdgeBuffer;
            var currentY = EdgeBuffer;

            for (int y = 0; y < vSquares; y++)
            {
                for (int x = 0; x < hSquares; x++)
                {
                    gfx.DrawRectangle(Pens.Gainsboro, currentX, currentY, PixelsPerSquare, PixelsPerSquare);
                    currentX += PixelsPerSquare;
                }

                currentY += PixelsPerSquare;
                currentX = EdgeBuffer;
            }
        }

        private Bitmap GetInitialBitmap(int x, int y)
        {
            var width = (x * PixelsPerSquare) + (EdgeBuffer * 2);
            var height = (y * PixelsPerSquare) + (EdgeBuffer * 2);
            var bitmap = new Bitmap(width, height);

            return bitmap;
        }
    }
}