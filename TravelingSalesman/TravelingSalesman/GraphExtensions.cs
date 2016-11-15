namespace TravelingSalesman
{
    internal static class GraphExtensions
    {
        public static Node ToSkewed(this Node node, int xSkew = 0, int ySkew = 0)
        {
            var adjusted = node.Clone();
            adjusted.X += xSkew;
            adjusted.Y += ySkew;
            return adjusted;
        }
    }
}