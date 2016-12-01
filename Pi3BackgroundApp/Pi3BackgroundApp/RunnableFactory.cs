using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    
    internal class TestRunnable : IRunnable
    {
        private readonly LedRgb _ledRgb;

        public TestRunnable(LedRgb ledRgb)
        {
            _ledRgb = ledRgb;
        }

        public async Task Run()
        {
            for (int i = 0; i < 100; i++)
            {
                _ledRgb.Send(new RgbCommand { Green = i%2==0 });
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
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
