using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default
{
    public sealed class EnchantmentFactory : IEnchantmentFactory
    {
        private readonly IGameObjectFactory _gameObjectFactory;

        public EnchantmentFactory(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
        }

        public IGameObject Create(IEnumerable<IBehavior> behaviors)
        {
            if (behaviors
                .TakeTypes<IHasStatDefinitionIdBehavior>()
                .SingleOrDefault() == null)
            {
                throw new ArgumentException($"Missing required '{typeof(IHasStatDefinitionIdBehavior)}'.");
            }

            if (behaviors
                .TakeTypes<IEnchantmentTargetBehavior>()
                .SingleOrDefault() == null)
            {
                throw new ArgumentException($"Missing required '{typeof(IEnchantmentTargetBehavior)}'.");
            }

            var enchantment = _gameObjectFactory.Create(behaviors);
            return enchantment;
        }
    }
}