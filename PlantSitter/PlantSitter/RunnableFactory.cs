using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;

namespace PlantSitter
{
    internal class RunnableFactory : IRunnableFactory
    {
        public IRunnable GetRunnable(IBackgroundTaskInstance taskInstance)
        {

            return new TestLedRunnable(taskInstance);
        }
    }

    internal class TestLedRunnable : IRunnable
    {
        private bool _isInitialized = false;

        private GpioPin Red { get; set; }
        private GpioPin Green { get; set; }
        private GpioPin Blue { get; set; }

        public TestLedRunnable(IBackgroundTaskInstance taskInstance)
        {
        }

        public async Task Run()
        {
            await Init();

            Green.Write(GpioPinValue.High);
        }

        private async Task Init()
        {
            if (!_isInitialized)
            {
                var gpio = await GpioController.GetDefaultAsync();

                Red = gpio.OpenPin(2);
                Red.SetDriveMode(GpioPinDriveMode.Output);
                Green = gpio.OpenPin(3);
                Green.SetDriveMode(GpioPinDriveMode.Output);
                Blue = gpio.OpenPin(4);
                Blue.SetDriveMode(GpioPinDriveMode.Output);

                _isInitialized = true;
            }
        }
    }
}