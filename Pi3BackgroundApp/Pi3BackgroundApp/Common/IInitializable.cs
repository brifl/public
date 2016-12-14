using System.Threading.Tasks;

namespace Pi3BackgroundApp.Common
{
    internal interface IInitializable
    {
        bool IsInitialized { get; set; }

        Task Initialize();
    }
}
