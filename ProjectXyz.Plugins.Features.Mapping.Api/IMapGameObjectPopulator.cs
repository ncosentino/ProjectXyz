using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapGameObjectPopulator
    {
        Task PopulateMapGameObjectsAsync(IGameObject map);
    }
}