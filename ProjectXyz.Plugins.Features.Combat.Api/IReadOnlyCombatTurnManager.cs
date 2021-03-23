using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface IReadOnlyCombatTurnManager
    {
        IEnumerable<IGameObject> GetSnapshot(
            IFilterContext filterContext,
            int length);
    }
}
