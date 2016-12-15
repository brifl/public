using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Pi3BackgroundApp.Common;
using Pi3BackgroundApp.Devices;

namespace Pi3BackgroundApp.Prototyping
{
    internal class TestRunnable2 : IRunnable
    {
        private static readonly string IotHostName = "bripi3.azure-devices.net";
        private static readonly string DeviceId = "bripi3";
        private static readonly string DeviceKey = "";
        private readonly IPollable<TemperatureHumidity> _tempHumidity;

        private readonly Lazy<DeviceClient> Client = new Lazy<DeviceClient>(CreateClient);

        public TestRunnable2(IPollable<TemperatureHumidity> tempHumidity)
        {
            _tempHumidity = tempHumidity;
        }

        public async Task Run()
        {
            var th = _tempHumidity.GetValue();


            var str = $"-TEST-\tTemp:{th.Temperature.DegreesFarenheight}\tHumidity:{th.Humidity.RHPercent}";

            var message = CreateMessage(str);

            await Client.Value.SendEventAsync(message);

            Debug.WriteLine("Sent " + str);
        }

        private static DeviceClient CreateClient()
        {
            var deviceClient = DeviceClient.Create(IotHostName,
                AuthenticationMethodFactory.
                    CreateAuthenticationWithRegistrySymmetricKey(DeviceId, DeviceKey),
                TransportType.Http1);

            return deviceClient;
        }

        private Message CreateMessage(string message)
        {
            return new Message(Encoding.ASCII.GetBytes(message));
        }
    }
}
