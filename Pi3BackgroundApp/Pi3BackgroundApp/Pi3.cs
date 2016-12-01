using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Gpio;
using Windows.Devices.SerialCommunication;
using Windows.Devices.Spi;

namespace Pi3BackgroundApp
{
    internal class Pi3
    {
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio2 = new Gpio(2);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio3 = new Gpio(3);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio4 = new Gpio(4);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio5 = new Gpio(5);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio6 = new Gpio(6);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio7 = new Gpio(7);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio8 = new Gpio(8);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio9 = new Gpio(9);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio10 = new Gpio(10);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio11 = new Gpio(11);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio12 = new Gpio(12);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio13 = new Gpio(13);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio16 = new Gpio(16);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio17 = new Gpio(17);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio18 = new Gpio(18);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio19 = new Gpio(19);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio20 = new Gpio(20);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio21 = new Gpio(21);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio22 = new Gpio(22);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio23 = new Gpio(23);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio24 = new Gpio(24);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio25 = new Gpio(25);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio26 = new Gpio(26);
        public static IBoardResourceProvider1<GpioPin, GpioPinDriveMode> Gpio27 = new Gpio(27);

        public static IBoardResourceProvider<SerialDevice> Uart0 = new Uart("UART0");
        public static IBoardResourceProvider<SerialDevice> Uart1 = new Uart("UART1");

        public static IBoardResourceProvider<SpiDevice> Spi0 = new Spi(0);
        public static IBoardResourceProvider<SpiDevice> Spi1 = new Spi(1);

        public class Gpio : IBoardResourceProvider1<GpioPin, GpioPinDriveMode>
        {
            private readonly int _pinNumber;

            internal Gpio(int pinNumber)
            {
                _pinNumber = pinNumber;
            }

            public async Task<GpioPin> GetAsync(GpioPinDriveMode driveMode)
            {
                var controller = await GpioController.GetDefaultAsync();
                var pin = controller.OpenPin(_pinNumber);
                pin.SetDriveMode(driveMode);

                return pin;
            }
        }

        public class Uart : IBoardResourceProvider<SerialDevice>
        {
            private readonly string _id;

            internal Uart(string id)
            {
                _id = id;
            }

            public async Task<SerialDevice> GetAsync()
            {
                var selector = SerialDevice.GetDeviceSelector(_id);
                var deviceInfo = await DeviceInformation.FindAllAsync(selector);
                var device = await SerialDevice.FromIdAsync(deviceInfo[0].Id);

                return device;
            }
        }

        private class Spi : IBoardResourceProvider<SpiDevice>
        {
            private readonly int _id;

            internal Spi(int id)
            {
                _id = id;
            }

            public async Task<SpiDevice> GetAsync()
            {
                var settings = new SpiConnectionSettings(_id);
                var device = await SpiDevice.FromIdAsync(_id.ToString(), settings);
                return device;
            }
        }
    }
}
