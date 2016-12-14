using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Pi3BackgroundApp.Common
{
    internal class ResilientRunner : IRunnable
    {
        private readonly IRunnable _runnable;

        public ResilientRunner(IRunnable runnable)
        {
            _runnable = runnable;
        }

        public async Task Run()
        {
            try
            {
                await _runnable.Run();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}