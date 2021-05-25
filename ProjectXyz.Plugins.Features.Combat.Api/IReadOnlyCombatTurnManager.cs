using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface IReadOnlyCombatTurnManager
    {
        bool InCombat { get; }

        IEnumerable<IGameObject> GetSnapshot(
            IFilterContext filterContext,
            int length);
    }
}
