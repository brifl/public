using System;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.Background;
using Pi3BackgroundApp.Common;

namespace Pi3BackgroundApp
{
    public sealed class StartupTask : IBackgroundTask
    {
        private readonly IRunnableFactory _runnables = new RunnableFactory();
        private BackgroundTaskDeferral _deferral;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnCancel;
            try
            {
                var runnable = _runnables.GetRunnable(taskInstance);
                await runnable.Run();
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Exiting. Failed with exception: {e.Message}");
                Debug.WriteLine(e);
                OnCancel(taskInstance, BackgroundTaskCancellationReason.Terminating);
            }
        }

        private void OnCancel(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            Debug.WriteLine($"Shutting down with reason {Enum.GetName(typeof(BackgroundTaskCancellationReason), reason)}");
            try
            {
                _runnables.Dispose();
            }
            finally
            {
                _deferral.Complete();
            }
        }
    }
}
