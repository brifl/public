using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TravelingSalesman
{
    internal class Validator
    {
        public void Validate(HashSet<Node> allNodes, IEnumerable<Node> walk)
        {
            var walkHash = new HashSet<Node>();
            foreach (var node in walk)
            {
                walkHash.Add(node);
            }

            Assert.AreEqual(allNodes.Count, walkHash.Count);

            foreach (var node in allNodes)
            {
                Assert.IsTrue(walkHash.Contains(node));
            }
        }
    }
}