using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IMapGameObjectPopulator
    {
        Task PopulateMapGameObjectsAsync(IGameObject map);
    }
}