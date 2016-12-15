using System;
using Pi3BackgroundApp.Common;
using Pi3BackgroundApp.Devices;

namespace Pi3BackgroundApp
{
    internal class TemperatureHumiditySensor : IPollable<TemperatureHumidity>
    {
        private readonly ArduinoI2C _arduino;

        public TemperatureHumiditySensor(ArduinoI2C arduino)
        {
            _arduino = arduino;
        }

        public TemperatureHumidity GetValue()
        {
            var result = _arduino.GetValue();
            var tempHumidity = new TemperatureHumidity();

            var tempC = Convert.ToSingle(result["t"].GetNumber());
            tempHumidity.Temperature.DegreesCelsius = tempC;

            var humidity = Convert.ToSingle(result["h"].GetNumber());
            tempHumidity.Humidity.RHPercent = humidity;

            return tempHumidity;
        }
    }
}