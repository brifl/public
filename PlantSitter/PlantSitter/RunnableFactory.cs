using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace PlantSitter
{
    internal class RunnableFactory : IRunnableFactory
    {
        public IRunnable GetRunnable(IBackgroundTaskInstance taskInstance)
        {
            var instances = GetInstances(taskInstance);
            return new InitializedRunnable(instances);
        }

        private IEnumerable<object> GetInstances(IBackgroundTaskInstance taskInstance)
        {
            var instances = new List<object>();

            instances.Add(new LedRgb("TestLED", Pi3.Gpio2Out, Pi3.Gpio3Out, Pi3.Gpio4Out));
            return instances;
        }
    }

    internal class TestRunnable : IRunnable
    {
        public Task Run()
        {
            throw new System.NotImplementedException();
        }
    }
}
//ApplicationTriggerDetails;
//AppServiceTriggerDetails;
//ActivitySensorTriggerDetails;
//SensorDataThresholdTriggerDetails;
//AppointmentDataProviderTriggerDetails;
//AppointmentStoreNotificationTriggerDetails;
//BluetoothLEAdvertisementPublisherTriggerDetails;
//BluetoothLEAdvertisementWatcherTriggerDetails;
//RfcommConnectionTriggerDetails;
//DeviceWatcherTriggerDetails;
//EmailStoreNotificationTriggerDetails;
//SocketActivityTriggerDetails;
//SmsMessageReceivedTriggerDetails;
//WebAccountProviderTriggerDetails;