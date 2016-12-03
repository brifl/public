using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Spi;

namespace Pi3BackgroundApp
{
    /// <summary>
    /// DHT11 temperature and humidity sensor
    /// Configuration
    ///    5V power
    ///    5k pull up resistor (data-resistor-power)
    ///8bit integral RH data + 8bit decimal RH data + 8bit integral T data + 8bit decimal T data + 8bit check sum
    /// </summary>
    internal class TempHumiditySensor : IPollable<TemperatureHumidity>, IInitializable
    {
        private readonly IBoardResourceProvider1<SpiDevice, Action<SpiConnectionSettings>> _spiBus;
        private readonly IDataBitConverter _dataBitConverter;

        private static class BytePosition
        {
            public const int IntegralHumidity = 0;
            public const int DecimalHumidity = 1;
            public const int IntegralTempC = 2;
            public const int DecimalTempC = 3;
            public const int Checksum = 4;
        }

        private SpiDevice Spi { get; set; }

        public TempHumiditySensor(IBoardResourceProvider1<SpiDevice, Action<SpiConnectionSettings>> spiBus,
            IDataBitConverter dataBitConverter)
        {
            _spiBus = spiBus;
            _dataBitConverter = dataBitConverter;
        }

        public TemperatureHumidity GetValue()
        {
            var data = ReadFromSensor();


            var tAndH = new TemperatureHumidity();

            return tAndH;
        }

        private byte[] ReadFromSensor()
        {
            var data = new byte[5];
            Spi.Read(data);
            Task.Delay(140).Wait(); //recommended time for read
            VerifyChecksum(data);

            var fixedOrder = _dataBitConverter.FixEndianness(data, true);
            return fixedOrder;
        }

        private void VerifyChecksum(byte[] data)
        {
            var expected = data[BytePosition.DecimalHumidity];
            unchecked
            {
                expected += data[BytePosition.DecimalTempC];
                expected += data[BytePosition.IntegralHumidity];
                expected += data[BytePosition.IntegralTempC];
            }

            var actual = data[BytePosition.Checksum];

            if (actual != expected)
            {
                throw new DeviceReadFailedException("DHT11", "Checksum validation failed.");
            }
        }

        public bool IsInitialized { get; set; }

        public async Task Initialize()
        {
            Spi = await _spiBus.GetAsync(x =>
            {
                x.DataBitLength = 40;
            });
            await Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(x => IsInitialized = true);
        }
    }
}