using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public delegate T CombatCalculation<T>(
        IFilterContext filterContext,
        IReadOnlyCollection<IGameObject> actors,
        IGameObject currentActor);
}
