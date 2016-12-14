using System.Threading.Tasks;

namespace Pi3BackgroundApp.Common
{
    internal class ScheduledRunnable : IRunnable
    {
        private readonly IRunnable _runnable;
        private readonly ISchedule _schedule;

        public ScheduledRunnable(IRunnable runnable, ISchedule schedule)
        {
            _runnable = runnable;
            _schedule = schedule;
        }

        public Task Run()
        {
            _schedule.RunWhenDue(()=> _runnable.Run());

            return Task.CompletedTask;
        }
    }
}