using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace PlantSitter
{
    internal class LedRgb : IOutputDevice<RgbCommand>
    {
        private readonly IBoardResourceProvider1<GpioPin, GpioPinDriveMode> _blueOut;
        private readonly IBoardResourceProvider1<GpioPin, GpioPinDriveMode> _greenOut;
        private readonly IBoardResourceProvider1<GpioPin, GpioPinDriveMode> _redOut;

        private GpioPin RedPin { get; set; }
        private GpioPin GreenPin { get; set; }
        private GpioPin BluePin { get; set; }

        public LedRgb(
            IBoardResourceProvider1<GpioPin, GpioPinDriveMode> redOut,
            IBoardResourceProvider1<GpioPin, GpioPinDriveMode> greenOut,
            IBoardResourceProvider1<GpioPin, GpioPinDriveMode> blueOut,
            string name = null)
        {
            Name = NameGenerator.NameFor<LedRgb>(name);
            _redOut = redOut;
            _greenOut = greenOut;
            _blueOut = blueOut;
        }

        public void Send(RgbCommand message)
        {
            SetColor(RedPin, message.Red);
        }

        public bool IsInitialized { get; set; }

        public async Task Initialize()
        {
            RedPin = await _redOut.GetAsync(GpioPinDriveMode.Output);
            SetColor(RedPin, false);
            GreenPin = await _greenOut.GetAsync(GpioPinDriveMode.Output);
            SetColor(GreenPin, false);
            BluePin = await _blueOut.GetAsync(GpioPinDriveMode.Output);
            SetColor(BluePin, false);
        }

        public string Name { get; }

        private static void SetColor(GpioPin pin, bool isOn)
        {
            var newValue = isOn ? GpioPinValue.High : GpioPinValue.Low;
            if (pin.Read() != newValue)
            {
                pin.Write(newValue);
            }
        }
    }
}
