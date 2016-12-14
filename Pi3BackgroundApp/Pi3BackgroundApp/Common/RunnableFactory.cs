using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Background;

namespace Pi3BackgroundApp.Common
{
    internal class RunnableFactory : IRunnableFactory
    {
        private readonly PlantSitterDependencies _dependencies = new PlantSitterDependencies();
        private IDisposable _disposableInstances;

        public IRunnable GetRunnable(IBackgroundTaskInstance taskInstance)
        {
            _dependencies.Initialize();
            var instances = GetInstances(taskInstance);
            _disposableInstances = new AggregateDisposable(instances);

            return new InitializedRunnable(instances);
        }

        private IEnumerable<object> GetInstances(IBackgroundTaskInstance taskInstance)
        {
            return _dependencies.ResolveAll();
        }

        public void Dispose()
        {
            _disposableInstances?.Dispose();
            _disposableInstances = null;
        }
    }
}
