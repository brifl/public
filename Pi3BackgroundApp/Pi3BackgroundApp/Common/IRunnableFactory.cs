using System;
using Windows.ApplicationModel.Background;

namespace Pi3BackgroundApp.Common
{
    internal interface IRunnableFactory : IDisposable
    {
        IRunnable GetRunnable(IBackgroundTaskInstance taskInstance);
    }
}
