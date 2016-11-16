using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace PlantSitter
{
    internal class LedRgb : IOutputDevice<RgbCommand>
    {
        private readonly Pin<GpioPin> _blueOut;
        private readonly Pin<GpioPin> _greenOut;
        private readonly Pin<GpioPin> _redOut;

        private GpioPin RedPin { get; set; }
        private GpioPin GreenPin { get; set; }
        private GpioPin BluePin { get; set; }

        public LedRgb(string name, Pin<GpioPin> redOut, Pin<GpioPin> greenOut, Pin<GpioPin> blueOut)
        {
            Name = name;
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
            RedPin = await _redOut.GetAsync();
            SetColor(RedPin, false);
            GreenPin = await _greenOut.GetAsync();
            SetColor(GreenPin, false);
            BluePin = await _blueOut.GetAsync();
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
