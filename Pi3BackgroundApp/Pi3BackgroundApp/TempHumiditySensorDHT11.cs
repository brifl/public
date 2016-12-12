using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Devices.Spi;

namespace Pi3BackgroundApp
{
    /// <summary>
    /// DHT11 temperature and humidity sensor
     ///8bit integral RH data + 8bit decimal RH data + 8bit integral T data + 8bit decimal T data + 8bit check sum
    /// </summary>
    internal class TempHumiditySensorDHT11 : IPollable<TemperatureHumidity>, IInitializable
    {
        private readonly IBoardResourceProvider1<GpioPin, GpioPinDriveMode> _dataPin;
        private readonly IDataBitConverter _dataBitConverter;
        private readonly IDelay _delay;
        private readonly IPollable<DateTime> _timeProvider;
        private MinWaiter _minWaiter;

        private static class BytePosition
        {
            public const int IntegralHumidity = 0;
            public const int DecimalHumidity = 1;
            public const int IntegralTempC = 2;
            public const int DecimalTempC = 3;
            public const int Checksum = 4;
        }

        private GpioPin Gpio { get; set; }

        public TempHumiditySensorDHT11(IBoardResourceProvider1<GpioPin, GpioPinDriveMode> dataPin,
            IDataBitConverter dataBitConverter, IDelay delay, IPollable<DateTime> timeProvider)
        {
            _dataPin = dataPin;
            _dataBitConverter = dataBitConverter;
            _delay = delay;
            _timeProvider = timeProvider;
            _minWaiter = delay.GetWaiter(TimeSpan.FromSeconds(1));
        }

        public TemperatureHumidity GetValue()
        {
            _minWaiter.Wait();
            
            var data = ReadFromSensor();


            var tAndH = new TemperatureHumidity();
            
            return tAndH;
        }
        
        private byte[] ReadFromSensor()
        {
            PrepareForRead();

            var data = ReadBytes();
            VerifyChecksum(data);

            var fixedOrder = _dataBitConverter.FixEndianness(data, true);
            return fixedOrder;
        }

        private byte[] ReadBytes()
        {
            var stopTime = _timeProvider.GetValue().Ticks + 80;
            var bits = new BitBuffer(40);

            while (_timeProvider.GetValue().Ticks < stopTime)
            {
                
            }

            //for (i = 0; i < MAXTIMINGS; i++)
            //{
            //    counter = 0;
            //    while (digitalRead(DHTPIN) == laststate)
            //    {
            //        counter++;
            //        delayMicroseconds(1);
            //        if (counter == 255)
            //        {
            //            break;
            //        }
            //    }
            //    laststate = digitalRead(DHTPIN);

            //    if (counter == 255)
            //        break;

            //    /* ignore first 3 transitions */
            //    if ((i >= 4) && (i % 2 == 0))
            //    {
            //        /* shove each bit into the storage bytes */
            //        dht11_dat[j / 8] <<= 1;
            //        if (counter > 16)
            //            dht11_dat[j / 8] |= 1;
            //        j++;
            //    }
            //}

            return bits.GetBytes();
        }

        private void PrepareForRead()
        {
            Gpio.SetDriveMode(GpioPinDriveMode.Output);
            Gpio.Write(GpioPinValue.Low);
            _delay.Milliseconds(18);
            Gpio.Write(GpioPinValue.High);
            _delay.Microseconds(40);
            Gpio.SetDriveMode(GpioPinDriveMode.Input);
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
            Gpio = await _dataPin.GetAsync(GpioPinDriveMode.Output);
            _minWaiter.Reset();
            IsInitialized = true;
        }
    }
}