using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States
{
    public interface IReadOnlyStateManager
    {
        IReadOnlyDictionary<IIdentifier, object> GetStates(IIdentifier stateTypeId);
    }
}
