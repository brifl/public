using System.Threading.Tasks;

namespace Pi3BackgroundApp
{
    internal class TaskUtil
    {
        public static Task Empty => Task.FromResult(new object());
    }
}