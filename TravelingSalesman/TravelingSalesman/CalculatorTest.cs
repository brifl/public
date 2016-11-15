using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TravelingSalesman
{
    [TestClass]
    public class CalculatorTest
    {
        private static readonly Calculator Calculator = new Calculator();

        [TestMethod]
        public void IsRayCrossingUpSlant()
        {
            var upperLeft = new Node {X = 11, Y = 16};
            var lowerRight = new Node {X = 13, Y = 12};
            var border = new Node {X = 12, Y = 14};

            var segmentStart = new Node {X = 10, Y = 10};
            var segmentEnd = new Node {X = 14, Y = 18};

            Assert.AreEqual(IntersectionType.Intersecting, Calculator.GetRayIntersectionType(upperLeft, segmentStart, segmentEnd));
            Assert.AreEqual(IntersectionType.None, Calculator.GetRayIntersectionType(lowerRight, segmentStart, segmentEnd));
            Assert.AreEqual(IntersectionType.LineBorder, Calculator.GetRayIntersectionType(border, segmentStart, segmentEnd));

            //var edges = new [] { new Edge {NodeA = segmentStart, NodeB = segmentEnd} };
            //var nodes = new [] {upperLeft, lowerRight, border};
            //Repository.SaveImage(edges, nodes);
        }

        [TestMethod]
        public void IsRayCrossingDownSlant()
        {
            var lowerLeft = new Node {X = 11, Y = 11};
            var upperRight = new Node {X = 13, Y = 16};
            var border = new Node {X = 12, Y = 14};

            var segmentStart = new Node {X = 10, Y = 18};
            var segmentEnd = new Node {X = 14, Y = 10};

            //var edges = new[] { new Edge { NodeA = segmentStart, NodeB = segmentEnd } };
            //var nodes = new[] { border, upperRight, lowerLeft };
            //Repository.SaveImage(edges, nodes);

            Assert.AreEqual(IntersectionType.Intersecting, Calculator.GetRayIntersectionType(lowerLeft, segmentStart, segmentEnd));
            Assert.AreEqual(IntersectionType.None, Calculator.GetRayIntersectionType(upperRight, segmentStart, segmentEnd));
            Assert.AreEqual(IntersectionType.LineBorder, Calculator.GetRayIntersectionType(border, segmentStart, segmentEnd));
        }

        [TestMethod]
        public void IsIntersectingMatrix()
        {
            var xSkew = 5;
            var ySkew = 3;

            var bottomLeftShort = new Node {X = 0, Y = 0}.ToSkewed(xSkew, ySkew);
            var topLeftShort = new Node {X = 0, Y = 4}.ToSkewed(xSkew, ySkew);
            var bottomRightShort = new Node {X = 8, Y = 0}.ToSkewed(xSkew, ySkew);
            var topRightShort = new Node {X = 8, Y = 4}.ToSkewed(xSkew, ySkew);

            var shortBoxUpSlant = new Edge {NodeA = bottomLeftShort, NodeB = topRightShort};
            var shortBoxDownSlant = new Edge {NodeA = topLeftShort, NodeB = bottomRightShort};

            var bottomLeftTall = new Node {X = 0, Y = 0}.ToSkewed(xSkew, ySkew);
            var topLeftTall = new Node {X = 0, Y = 8}.ToSkewed(xSkew, ySkew);
            var bottomRightTall = new Node {X = 4, Y = 0}.ToSkewed(xSkew, ySkew);
            var topRightTall = new Node {X = 4, Y = 8}.ToSkewed(xSkew, ySkew);

            var tallBoxUpSlant = new Edge {NodeA = bottomLeftTall, NodeB = topRightTall};
            var tallBoxDownSlant = new Edge {NodeA = topLeftTall, NodeB = bottomRightTall};

            VerifyIsRayCrossing(shortBoxUpSlant, Expect(xSkew, ySkew)
                .And(IntersectionType.Intersecting, 1, 3).And(IntersectionType.Intersecting, 2, 3).And(IntersectionType.Intersecting, 3, 3).And(IntersectionType.Intersecting, 4, 3).And(IntersectionType.Intersecting, 5, 3).And(IntersectionType.LineBorder, 6, 3).And(IntersectionType.None, 7, 3)
                .And(IntersectionType.Intersecting, 1, 2).And(IntersectionType.Intersecting, 2, 2).And(IntersectionType.Intersecting, 3, 2).And(IntersectionType.LineBorder, 4, 2).And(IntersectionType.None, 5, 2).And(IntersectionType.None, 6, 2).And(IntersectionType.None, 7, 2)
                .And(IntersectionType.Intersecting, 1, 1).And(IntersectionType.LineBorder, 2, 1).And(IntersectionType.None, 3, 1).And(IntersectionType.None, 4, 1).And(IntersectionType.None, 5, 1).And(IntersectionType.None, 6, 1).And(IntersectionType.None, 7, 1)
                );
            VerifyIsRayCrossing(shortBoxDownSlant, Expect(xSkew, ySkew)
                .And(IntersectionType.Intersecting, 1, 3).And(IntersectionType.LineBorder, 2, 3).And(IntersectionType.None, 3, 3).And(IntersectionType.None, 4, 3).And(IntersectionType.None, 5, 3).And(IntersectionType.None, 6, 3).And(IntersectionType.None, 7, 3)
                .And(IntersectionType.Intersecting, 1, 2).And(IntersectionType.Intersecting, 2, 2).And(IntersectionType.Intersecting, 3, 2).And(IntersectionType.LineBorder, 4, 2).And(IntersectionType.None, 5, 2).And(IntersectionType.None, 6, 2).And(IntersectionType.None, 7, 2)
                .And(IntersectionType.Intersecting, 1, 1).And(IntersectionType.Intersecting, 2, 1).And(IntersectionType.Intersecting, 3, 1).And(IntersectionType.Intersecting, 4, 1).And(IntersectionType.Intersecting, 5, 1).And(IntersectionType.LineBorder, 6, 1).And(IntersectionType.None, 7, 1)
                );
            VerifyIsRayCrossing(tallBoxUpSlant, Expect(xSkew, ySkew)
                .And(IntersectionType.Intersecting, 1, 7).And(IntersectionType.Intersecting, 2, 7).And(IntersectionType.Intersecting, 3, 7)
                .And(IntersectionType.Intersecting, 1, 6).And(IntersectionType.Intersecting, 2, 6).And(IntersectionType.LineBorder, 3, 6)
                .And(IntersectionType.Intersecting, 1, 5).And(IntersectionType.Intersecting, 2, 5).And(IntersectionType.None, 3, 5)
                .And(IntersectionType.Intersecting, 1, 4).And(IntersectionType.LineBorder, 2, 4).And(IntersectionType.None, 3, 4)
                .And(IntersectionType.Intersecting, 1, 3).And(IntersectionType.None, 2, 3).And(IntersectionType.None, 3, 3)
                .And(IntersectionType.LineBorder, 1, 2).And(IntersectionType.None, 2, 2).And(IntersectionType.None, 3, 2)
                .And(IntersectionType.None, 1, 1).And(IntersectionType.None, 2, 1).And(IntersectionType.None, 3, 1)
                );
            VerifyIsRayCrossing(tallBoxDownSlant, Expect(xSkew, ySkew)
                .And(IntersectionType.None, 1, 7).And(IntersectionType.None, 2, 7).And(IntersectionType.None, 3, 7)
                .And(IntersectionType.LineBorder, 1, 6).And(IntersectionType.None, 2, 6).And(IntersectionType.None, 3, 6)
                .And(IntersectionType.Intersecting, 1, 5).And(IntersectionType.None, 2, 5).And(IntersectionType.None, 3, 5)
                .And(IntersectionType.Intersecting, 1, 4).And(IntersectionType.LineBorder, 2, 4).And(IntersectionType.None, 3, 4)
                .And(IntersectionType.Intersecting, 1, 3).And(IntersectionType.Intersecting, 2, 3).And(IntersectionType.None, 3, 3)
                .And(IntersectionType.Intersecting, 1, 2).And(IntersectionType.Intersecting, 2, 2).And(IntersectionType.LineBorder, 3, 2)
                .And(IntersectionType.Intersecting, 1, 1).And(IntersectionType.Intersecting, 2, 1).And(IntersectionType.Intersecting, 3, 1)
                );
        }

        private static void VerifyIsRayCrossing(Edge segment, ExpectationBuilder expectations)
        {
            foreach (var expectation in expectations.Expectations)
            {
                var result = Calculator.GetRayIntersectionType(expectation.Point, segment.NodeA, segment.NodeB);
                var reverseResult = Calculator.GetRayIntersectionType(expectation.Point, segment.NodeB, segment.NodeA);
                var messagepart = $" for [{expectation.Point.X-expectations.XSkew}(+{expectations.XSkew}),{expectation.Point.Y-expectations.YSkew}(+{expectations.YSkew})]" +
                                  $" with line [[{segment.NodeA.X-expectations.XSkew}(+{expectations.XSkew}),{segment.NodeA.Y-expectations.YSkew}(+{expectations.YSkew})]," +
                                  $"[{segment.NodeB.X-expectations.XSkew}(+{expectations.XSkew}),{segment.NodeB.Y-expectations.YSkew}(+{expectations.YSkew})]]";
                Assert.AreEqual(result, reverseResult, "Result not revesible" + messagepart);
                Assert.AreEqual(expectation.ShouldIntersect, result, "Expectation not met" + messagepart);
                Console.WriteLine("Expectation met" + messagepart);
            }
        }

        private ExpectationBuilder Expect(int xSkew, int ySkew)
        {
            return new ExpectationBuilder(xSkew, ySkew);
        }

        [TestMethod]
        public void FailedIsIntersecting()
        {
            var node = new Node {X = 5, Y = 6};
            var edge = new Edge
            {
                NodeA = new Node
                {
                    X = 8,
                    Y = 1
                },
                NodeB = new Node
                {
                    X = 3,
                    Y = 7
                }
            };

            var calc = new Calculator();

            //var isIntersecting = calc.IsIntersectingRay(node, edge);


            var edges = new[] {edge};
            var nodes = new[] {node};
            Repository.SaveImage(edges, nodes);

            var isIntersecting = Calculator.GetRayIntersectionType(node, edge.NodeA, edge.NodeB);

            Assert.AreEqual(IntersectionType.None,isIntersecting);
        }

        private class ExpectationBuilder
        {
            public readonly int XSkew;
            public readonly int YSkew;
            public readonly List<Expectation> Expectations = new List<Expectation>();

            public ExpectationBuilder(int xSkew, int ySkew)
            {
                XSkew = xSkew;
                YSkew = ySkew;
            }

            public ExpectationBuilder ToBe(IntersectionType shouldIntersect, int x, int y)
            {
                var node = new Node {X = x + XSkew, Y = y + YSkew};
                Expectations.Add(new Expectation {Point = node, ShouldIntersect = shouldIntersect});
                return this;
            }

            public ExpectationBuilder And(IntersectionType shouldIntersect, int x, int y)
            {
                return ToBe(shouldIntersect, x, y);
            }

            public class Expectation
            {
                public Node Point;
                public IntersectionType ShouldIntersect;
            }
        }
    }
}