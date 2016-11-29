using System.Threading.Tasks;

namespace Pi3BackgroundApp
{
    internal interface IInitializable
    {
        bool IsInitialized { get; set; }

        Task Initialize();
    }
}
