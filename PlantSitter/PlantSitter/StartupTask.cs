using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace PlantSitter
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
            finally
            {
                deferral.Complete();
            }
        }
    }
}
