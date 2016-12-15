namespace Pi3BackgroundApp.Devices
{
    internal class TemperatureHumidity
    {
        public Temperature Temperature { get; } = new Temperature();
        public Humidity Humidity { get; } = new Humidity();
    }
}