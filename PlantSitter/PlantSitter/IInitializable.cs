using System.Threading.Tasks;

namespace PlantSitter
{
    internal interface IInitializable
    {
        bool IsInitialized { get; set; }

        Task Initialize();
    }
}
