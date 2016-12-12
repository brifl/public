using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pi3BackgroundApp.UnitTests
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

        [TestMethod]
        public void ByteLengthString()
        {
            var temperature = 100.1f;
            var humidity = 54.6;

            String json = "{\"t\":";
            json += temperature;
            json += ", \"h\":";
            json += humidity;
            json += "}";

            Console.WriteLine(json);
            Console.WriteLine(json.Length);
        }

        [TestMethod]
        public void CharLengths()
        {
            var specialChars = "{}\"',:azAz09".ToCharArray();
            Console.WriteLine((char)(new byte()));
            foreach (var c in specialChars)
            {
                var bytes = BitConverter.GetBytes(c);
                Console.WriteLine($"'{c}': [{bytes[0]}, {bytes[1]}] / {(char)bytes[0]}");
            }
        }

        [TestMethod]
        public void Precision()
        {
            var startDt = DateTime.UtcNow;
            var startTicks = startDt.Ticks;
            var sampleCount = 0;
            DateTime thisDt;
            while (true)
            {
                thisDt = DateTime.UtcNow;
                sampleCount++;
                if (thisDt.Ticks > startTicks)
                {
                    break;
                }
            }

            Console.WriteLine($"Samples: {sampleCount}");
            Console.WriteLine($"Start DT: {startDt.ToString("O")}\tTicks: {startTicks}");
            Console.WriteLine($"End DT: {thisDt.ToString("O")}\tTicks: {thisDt.Ticks}");
            Console.WriteLine($"Gap: {thisDt.Subtract(startDt).TotalMilliseconds} \tTicks: {thisDt.Ticks-startTicks}");
        }

        [TestMethod]
        public void ConvertFloat()
        {
            var floaty = BitConverter.ToDouble(new byte[]
            {
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                113
            }, 0);

            var floatyBytes = BitConverter.GetBytes(floaty);
            Console.WriteLine(floaty);
            
        }
    }
}
