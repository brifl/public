using System.Collections.Generic;
using Windows.ApplicationModel.Background;

namespace Pi3BackgroundApp
{
    internal class RunnableFactory : IRunnableFactory
    {
        private readonly PlantSitterDependencies _dependencies = new PlantSitterDependencies();

        public IRunnable GetRunnable(IBackgroundTaskInstance taskInstance)
        {
            _dependencies.Initialize();
            var instances = GetInstances(taskInstance);
            return new InitializedRunnable(instances);
        }

        private IEnumerable<object> GetInstances(IBackgroundTaskInstance taskInstance)
        {
            return _dependencies.ResolveAll();
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
