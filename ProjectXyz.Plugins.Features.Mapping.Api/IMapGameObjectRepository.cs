using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapGameObjectRepository
    {
        Task<IReadOnlyCollection<IGameObject>> LoadForMapAsync(IIdentifier mapId);
    }
}
