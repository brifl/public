using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

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
                Debug.WriteLine($"Total. Fail. {e.Message}");
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}
