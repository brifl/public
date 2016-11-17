using Windows.ApplicationModel.Background;

namespace PlantSitter
{
    internal interface IRunnableFactory
    {
        IRunnable GetRunnable(IBackgroundTaskInstance taskInstance);
    }
}
