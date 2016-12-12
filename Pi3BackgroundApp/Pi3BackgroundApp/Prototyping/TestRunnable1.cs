using System;
using System.Threading.Tasks;

namespace Pi3BackgroundApp.Prototyping
{
    internal class TestRunnable1 : IRunnable
    {
        private readonly LedRgb _ledRgb;

        public TestRunnable1(LedRgb ledRgb)
        {
            _ledRgb = ledRgb;
        }

        public async Task Run()
        {
            for (int i = 0; i < 100; i++)
            {
                var command = new RgbCommand
                {
                    Red = (i & 1) > 0,
                    Green = (i & 2) > 0,
                    Blue = (i & 4) > 0
                };

                if ((command.Red && command.Green && command.Blue) 
                    || (!command.Red && !command.Green && !command.Blue))
                {
                    continue;
                }

                _ledRgb.Send(command);

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}