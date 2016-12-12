using System;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Gpio;

namespace Pi3BackgroundApp
{
    internal class ArduinoI2C : IPollable<JsonObject>, IDevice
    {
        public const int BufferLength = 32;

        public ArduinoI2C(string name, IBoardResourceProvider1<GpioPin, GpioPinDriveMode> gpioProvider)
        {
            Name = name;
            GpioProvider = gpioProvider;
        }

        public JsonObject GetValue()
        {
            throw new NotImplementedException();
        }

        public bool IsInitialized { get; set; }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
        public IBoardResourceProvider1<GpioPin, GpioPinDriveMode> GpioProvider { get; set; }
    }
}