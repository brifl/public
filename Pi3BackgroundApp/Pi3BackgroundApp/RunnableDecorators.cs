namespace Pi3BackgroundApp
{
    internal static class RunnableDecorators
    {
        public static IRunnable AsResilient(this IRunnable runnable)
        {
            if (runnable is ResilientRunner)
            {
                return runnable;
            }

            return new ResilientRunner(runnable);
        }

        public static IRunnable AsScheduled(this IRunnable runnable, ISchedule schedule)
        {
            if (runnable is ScheduledRunnable)
            {
                return runnable;
            }

            return new ScheduledRunnable(runnable, schedule);
        }
    }
}