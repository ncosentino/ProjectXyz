using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Default
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IHasStatsBehaviorFactory _hasMutableStatsBehaviorFactory;

        public ItemFactory(
            IGameObjectFactory gameObjectFactory,
            IHasStatsBehaviorFactory hasMutableStatsBehaviorFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
        }

        public IGameObject Create(IEnumerable<IBehavior> behaviors)
        {
            var itemBehaviors = new List<IBehavior>();

            bool hasIdBehavior = false;
            bool hasStatsBehavior = false;
            foreach (var behavior in behaviors)
            {
                itemBehaviors.Add(behavior);
                hasIdBehavior |= behavior is IReadOnlyIdentifierBehavior;
                hasStatsBehavior |= behavior is IHasReadOnlyStatsBehavior;
            }

            if (!hasStatsBehavior)
            {
                itemBehaviors.Add(_hasMutableStatsBehaviorFactory.Create());
            }

            if (!hasIdBehavior)
            {
                itemBehaviors.Add(new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString())));
            }

            var item = _gameObjectFactory.Create(itemBehaviors);
            return item;
        }
    }
}