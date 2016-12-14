using System;
using System.Threading.Tasks;
using Pi3BackgroundApp.Common;

namespace Pi3BackgroundApp.Prototyping
{
    internal class TestRunnable2 : IRunnable
    {
        private readonly ArduinoI2C _arduino;

        public TestRunnable2(ArduinoI2C arduino)
        {
            _arduino = arduino;
        }

        public async Task Run()
        {
            var result = _arduino.GetValue();
            var tempHumidity = new TemperatureHumidity();

            var tempC = Convert.ToSingle(result["t"].GetNumber());
            tempHumidity.Temperature.DegreesCelsius = tempC;

            var humidity = Convert.ToSingle(result["h"].GetNumber());
            tempHumidity.Humidity.RHPercent = humidity;

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}