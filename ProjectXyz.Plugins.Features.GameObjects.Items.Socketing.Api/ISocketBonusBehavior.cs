using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api
{
    public interface ISocketBonusBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> Enchantments { get; }

        IReadOnlyCollection<IFilterAttribute> Filters { get; }
    }
}