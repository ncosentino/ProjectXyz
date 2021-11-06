using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class SocketBonusBehavior :
        BaseBehavior, 
        ISocketBonusBehavior
    {
        public SocketBonusBehavior(
            IEnumerable<IFilterAttribute> filters,
            IEnumerable<IGameObject> enchantments)
        {
            Filters = filters.ToArray();
            Enchantments = enchantments.ToArray();
        }

        public IReadOnlyCollection<IFilterAttribute> Filters { get; }

        public IReadOnlyCollection<IGameObject> Enchantments { get; }
    }
}