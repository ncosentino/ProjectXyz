using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapGameObjectPopulator
    {
        Task PopulateMapGameObjectsAsync(IIdentifier mapId);
    }
}