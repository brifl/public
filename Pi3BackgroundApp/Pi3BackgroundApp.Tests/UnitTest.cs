using System;
using Windows.UI.ViewManagement;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Pi3BackgroundApp;

namespace Pi3BackgroundApp.Tests
{
    [TestClass]
    public class DataBitConverterTest
    {
        [TestMethod]
        public void ReverseAndConvertInt()
        {
            var littleEndianInt = Convert.ToInt32("10110110000000000000000000000000", 2);
            var littleEndianBytes = BitConverter.GetBytes(littleEndianInt);

            var converter = new DataBitConverter();

            var bigEndianBytes = converter.FixEndianness(littleEndianBytes, true);

            var result = converter.ToInt(bigEndianBytes);

            var expectedInt = Convert.ToInt32("01101101", 2);

            Assert.AreEqual(expectedInt, result);
        }
    }
}
