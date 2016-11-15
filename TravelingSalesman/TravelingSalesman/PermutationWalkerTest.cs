using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TravelingSalesman
{
    [TestClass]
    public class PermutationWalkerTest
    {
        [TestMethod]
        public void PermutationEnumeration()
        {
            var testCount = 5;
            var collection = new string[testCount];
            for (int i = 0; i < testCount; i++)
            {
                collection[i] = i.ToString();
            }

            var enumerator = new TspPermuationWalker<string>(collection);

            var results = enumerator.ToArray();

            var permutations = 1;
            for (int i = 1; i < testCount; i++)
            {
                permutations *= i;
            }

            Assert.AreEqual(permutations, results.Length);

            var checkUnique = new HashSet<string>();

            foreach (var permutation in results)
            {
                Assert.AreEqual("0", permutation[0]);
                Assert.AreEqual(testCount, permutation.Length);
                var stringified = string.Join("-", permutation);
                if (checkUnique.Contains(stringified))
                {
                    Assert.Fail(stringified + " is not unique");
                }
                checkUnique.Add(stringified);
            }
        }
    }
}