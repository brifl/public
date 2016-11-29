using System.Threading.Tasks;

namespace Pi3BackgroundApp
{
    internal interface IBoardResourceProvider<TResource>
    {
        Task<TResource> GetAsync();
    }

    internal interface IBoardResourceProvider1<TResource, in TParam1>
    {
        Task<TResource> GetAsync(TParam1 param1);
    }
}
