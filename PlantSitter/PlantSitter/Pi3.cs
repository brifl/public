using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Gpio;
using Windows.Devices.SerialCommunication;
using Windows.Devices.Spi;

namespace PlantSitter
{
    internal class Pi3
    {
        public static Pin<GpioPin> Gpio2In = new Pin<GpioPin>(() => OpenGpio(2, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio2Out = new Pin<GpioPin>(() => OpenGpio(2, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio3In = new Pin<GpioPin>(() => OpenGpio(3, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio3Out = new Pin<GpioPin>(() => OpenGpio(3, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio4In = new Pin<GpioPin>(() => OpenGpio(4, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio4Out = new Pin<GpioPin>(() => OpenGpio(4, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio5In = new Pin<GpioPin>(() => OpenGpio(5, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio5Out = new Pin<GpioPin>(() => OpenGpio(5, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio6In = new Pin<GpioPin>(() => OpenGpio(6, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio6Out = new Pin<GpioPin>(() => OpenGpio(6, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio7In = new Pin<GpioPin>(() => OpenGpio(7, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio7Out = new Pin<GpioPin>(() => OpenGpio(7, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio8In = new Pin<GpioPin>(() => OpenGpio(8, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio8Out = new Pin<GpioPin>(() => OpenGpio(8, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio9In = new Pin<GpioPin>(() => OpenGpio(9, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio9Out = new Pin<GpioPin>(() => OpenGpio(9, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio10In = new Pin<GpioPin>(() => OpenGpio(10, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio10Out = new Pin<GpioPin>(() => OpenGpio(10, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio11In = new Pin<GpioPin>(() => OpenGpio(11, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio11Out = new Pin<GpioPin>(() => OpenGpio(11, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio12In = new Pin<GpioPin>(() => OpenGpio(12, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio12Out = new Pin<GpioPin>(() => OpenGpio(12, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio13In = new Pin<GpioPin>(() => OpenGpio(13, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio13Out = new Pin<GpioPin>(() => OpenGpio(13, GpioPinDriveMode.Output));

        public static Pin<GpioPin> Gpio16In = new Pin<GpioPin>(() => OpenGpio(16, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio16Out = new Pin<GpioPin>(() => OpenGpio(16, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio17In = new Pin<GpioPin>(() => OpenGpio(17, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio17Out = new Pin<GpioPin>(() => OpenGpio(17, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio18In = new Pin<GpioPin>(() => OpenGpio(18, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio18Out = new Pin<GpioPin>(() => OpenGpio(18, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio19In = new Pin<GpioPin>(() => OpenGpio(19, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio19Out = new Pin<GpioPin>(() => OpenGpio(19, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio20In = new Pin<GpioPin>(() => OpenGpio(20, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio20Out = new Pin<GpioPin>(() => OpenGpio(20, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio21In = new Pin<GpioPin>(() => OpenGpio(21, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio21Out = new Pin<GpioPin>(() => OpenGpio(21, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio22In = new Pin<GpioPin>(() => OpenGpio(22, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio22Out = new Pin<GpioPin>(() => OpenGpio(22, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio23In = new Pin<GpioPin>(() => OpenGpio(23, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio23Out = new Pin<GpioPin>(() => OpenGpio(23, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio24In = new Pin<GpioPin>(() => OpenGpio(24, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio24Out = new Pin<GpioPin>(() => OpenGpio(24, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio25In = new Pin<GpioPin>(() => OpenGpio(25, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio25Out = new Pin<GpioPin>(() => OpenGpio(25, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio26In = new Pin<GpioPin>(() => OpenGpio(26, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio26Out = new Pin<GpioPin>(() => OpenGpio(26, GpioPinDriveMode.Output));
        public static Pin<GpioPin> Gpio27In = new Pin<GpioPin>(() => OpenGpio(27, GpioPinDriveMode.Input));
        public static Pin<GpioPin> Gpio27Out = new Pin<GpioPin>(() => OpenGpio(27, GpioPinDriveMode.Output));

        public static Pin<SerialDevice> Uart0 = new Pin<SerialDevice>(() => OpenSerialUart("UART0"));
        public static Pin<SerialDevice> Uart1 = new Pin<SerialDevice>(() => OpenSerialUart("UART1"));

        public static Pin<SpiDevice> Spi0 = new Pin<SpiDevice>(() => OpenSpi(0));
        public static Pin<SpiDevice> Spi1 = new Pin<SpiDevice>(() => OpenSpi(1));

        private static async Task<GpioPin> OpenGpio(int pinNumber, GpioPinDriveMode driveMode)
        {
            var controller = await GpioController.GetDefaultAsync();
            var pin = controller.OpenPin(pinNumber);
            pin.SetDriveMode(driveMode);

            return pin;
        }

        private static async Task<SerialDevice> OpenSerialUart(string id)
        {
            var selector = SerialDevice.GetDeviceSelector(id); 
            var deviceInfo = await DeviceInformation.FindAllAsync(selector);
            var device = await SerialDevice.FromIdAsync(deviceInfo[0].Id);
            return device;
        }

        private static async Task<SpiDevice> OpenSpi(int id)
        {
            var settings = new SpiConnectionSettings(id);
            var controller = await SpiController.GetDefaultAsync();
            var device = controller.GetDevice(settings);
            return device;
        }
    }
}