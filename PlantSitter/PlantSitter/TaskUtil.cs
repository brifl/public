using System.Threading.Tasks;

namespace PlantSitter
{
    internal class TaskUtil
    {
        public static Task Empty => Task.FromResult(new object());
    }
}