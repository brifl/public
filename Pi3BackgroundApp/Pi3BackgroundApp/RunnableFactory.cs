using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Pi3BackgroundApp
{
    internal class RunnableFactory : IRunnableFactory
    {
        private readonly Pi3BackgroundAppDependencies _dependencies = new Pi3BackgroundAppDependencies();

        public IRunnable GetRunnable(IBackgroundTaskInstance taskInstance)
        {
            _dependencies.Initialize();
            var instances = GetInstances(taskInstance);
            return new InitializedRunnable(instances);
        }

        private IEnumerable<object> GetInstances(IBackgroundTaskInstance taskInstance)
        {
            var instances = new List<object>();

            instances.Add(_dependencies.Resolve<TestRunnable>());
            return instances;
        }
    }


    internal class TestRunnable : IRunnable
    {
        private readonly LedRgb _ledRgb;

        public TestRunnable(LedRgb ledRgb)
        {
            _ledRgb = ledRgb;
        }

        public Task Run()
        {
            _ledRgb.Send(new RgbCommand {Green = true});
            return TaskUtil.Empty;
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
