using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TravelingSalesman
{
    internal class Calculator
    {
        public double Distance(Node from, Node to)
        {
            return Distance(new Coordinate {X = from.X, Y = from.Y}, to);
        }

        public double Distance(Coordinate from, Node to)
        {
            var a = from.X - to.X;
            var b = from.Y - to.Y;
            var c2 = (a * a) + (b * b);
            return Math.Sqrt(c2);
        }

        public double AddedDistance(Edge edge, Node node)
        {
            var currentDistance = Distance(edge.NodeA, edge.NodeB);
            var newDistance = Distance(node, edge.NodeA) + Distance(node, edge.NodeB);
            return newDistance - currentDistance;
        }

        public double Distance(Node[] walk)
        {
            var distance = Distance(walk[0], walk[walk.Length - 1]);
            for (int i = 0; i < walk.Length - 1; i++)
            {
                distance += Distance(walk[i], walk[i + 1]);
            }

            return distance;
        }

        public double Area(Node a, Node b, Node c)
        {
            var numerator = (a.X*(b.Y - c.Y)) + (b.X*(c.Y - a.Y)) + (c.X*(a.Y - b.Y));
            return Math.Abs(numerator/2.0);
        }

        public double LengthC(Node a, Node b, Node c)
        {
            var area = Area(a, b, c);
            var baseDistance = Distance(a, b);
            return (2*area)/baseDistance;
        }

        public bool AreDoublesEqual(double a, double b, double threshold = 0.00001)
        {
            return Math.Abs(a - b) < threshold;
        }

        //Determines segment intersection if a ray along x axis starts from rayStart .---------> \
        public IntersectionType GetIntersectionType(Node ray, Edge segment)
        {
            var a = segment.NodeA;
            var b = segment.NodeB;

            //touching and parallel
            if (a.Y == ray.Y && b.Y == ray.Y)
            {
                if ((a.X > ray.X && b.X < ray.X) || (a.X > ray.X && b.X < ray.X)) /*ray start is between the segment points*/
                {
                    return IntersectionType.LineBorder;
                }

                return IntersectionType.None; /*treat as none because it is like 2 intersections*/
            }

            //touching - determine top or bottom
            if (a.Y == ray.Y && a.X >= ray.X)
            {
                return b.Y > a.Y ? IntersectionType.BottomBorder : IntersectionType.TopBorder;
            }
            if (b.Y == ray.Y && b.X >= ray.X)
            {
                return a.Y > b.Y ? IntersectionType.BottomBorder : IntersectionType.TopBorder;
            }

            //easy cases outside
            if ((a.X < ray.X && b.X < ray.X) /*segment is left*/ 
                    || a.Y > ray.Y && b.Y > ray.Y /*segment is above*/
                    || a.Y < ray.Y && b.Y < ray.Y /*segment is below*/) 
            {
                return IntersectionType.None;
            }
            
            //potential intersection - half is above and half is below
            if (a.X < ray.X || b.X < ray.X) //segment starts before ray starts - need to determine if it is left or right
            {
                return GetRayIntersectionType(ray, a, b);
            }

            return IntersectionType.Intersecting; //half above, half below, and starts after ray - it must intersect
        }

        //Exposed for unit testing - tricky code here
        internal IntersectionType GetRayIntersectionType(Node ray, Node a, Node b)
        {
            var left = a.X < b.X ? a : b;
            var right = left.Equals(a) ? b : a;

            var xAdjust = Math.Min(left.X, right.X);
            var yAdjust = Math.Min(left.Y, right.Y);

            var leftX = left.X - xAdjust;
            var leftY = left.Y - yAdjust;
            var rightX = right.X - xAdjust;
            var rightY = right.Y - yAdjust;
            var rayX = ray.X - xAdjust;
            var rayY = ray.Y - yAdjust;

            double dx = rightX - leftX;
            double dy = rightY - leftY;

            var isUpslope = dy > 0;

            var slope = Math.Abs(dx/dy);

            var lineXatY = isUpslope ? rayY*slope : rightX - rayY*slope;

            if (AreDoublesEqual(rayX, lineXatY))
            {
                return IntersectionType.LineBorder;
            }

            return rayX < lineXatY ? IntersectionType.Intersecting : IntersectionType.None;
        }
    }

    public enum IntersectionType
    {
        None,
        Intersecting,
        LineBorder,
        TopBorder,
        BottomBorder
    }
}