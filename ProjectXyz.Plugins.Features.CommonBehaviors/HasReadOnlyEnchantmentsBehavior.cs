using System.Collections.Generic;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasReadOnlyEnchantmentsBehavior :
        BaseBehavior,
        IHasReadOnlyEnchantmentsBehavior
    {
        private readonly IActiveEnchantmentManager _activeEnchantmentManager;

        public HasReadOnlyEnchantmentsBehavior(IActiveEnchantmentManager activeEnchantmentManager)
        {
            _activeEnchantmentManager = activeEnchantmentManager;
        }

        public IReadOnlyCollection<IGameObject> Enchantments => _activeEnchantmentManager.Enchantments;
    }
}