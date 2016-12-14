using System;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.Background;

namespace Pi3BackgroundApp
{
    public sealed class StartupTask : IBackgroundTask
    {
        private readonly IRunnableFactory _runnables = new RunnableFactory();

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                var runnable = _runnables.GetRunnable(taskInstance);
                await runnable.Run();
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Failed with exception: {e.Message}");
                Debug.WriteLine(e);
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}
