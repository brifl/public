using Windows.ApplicationModel.Background;

namespace PlantSitter
{
    internal class RunnableFactory : IRunnableFactory
    {
        public IRunnable GetRunnable(IBackgroundTaskInstance taskInstance)
        {
            return new TestLedRunnable(taskInstance);
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