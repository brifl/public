using System;
using Windows.ApplicationModel.Background;

namespace PlantSitter
{
    internal class RunnableFactory : IRunnableFactory
    {
        public IRunnable GetRunnable(IBackgroundTaskInstance taskInstance)
        {
            throw new NotImplementedException();
        }
    }
}