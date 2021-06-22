using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IMapStateRepository
    {
        IEnumerable<KeyValuePair<IIdentifier, IReadOnlyCollection<IIdentifier>>> GetAllState();

        IEnumerable<IIdentifier> GetState(IIdentifier mapId);

        bool HasState(IIdentifier mapId);

        void SaveState(
            IIdentifier mapId,
            IEnumerable<IIdentifier> gameObjectIds);

        void ClearState();
    }
}
