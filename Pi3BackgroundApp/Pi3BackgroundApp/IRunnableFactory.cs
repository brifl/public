using Windows.ApplicationModel.Background;

namespace Pi3BackgroundApp
{
    internal interface IRunnableFactory
    {
        IRunnable GetRunnable(IBackgroundTaskInstance taskInstance);
    }
}
