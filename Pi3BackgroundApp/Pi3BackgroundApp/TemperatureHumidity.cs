namespace Pi3BackgroundApp
{
    internal class TemperatureHumidity
    {
        public Temperature Temperature { get; } = new Temperature();
        public Humidity Humidity { get; } = new Humidity();
    }
}