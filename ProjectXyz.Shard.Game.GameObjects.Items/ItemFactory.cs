using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IHasMutableStatsBehaviorFactory _hasMutableStatsBehaviorFactory;

        public ItemFactory(
            IGameObjectFactory gameObjectFactory,
            IHasMutableStatsBehaviorFactory hasMutableStatsBehaviorFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
        }

        public IGameObject Create(IEnumerable<IBehavior> behaviors)
        {
            var hasMutableStats = _hasMutableStatsBehaviorFactory.Create();
            var idBehavior = new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString()));

            var item = _gameObjectFactory.Create(
                new IBehavior[]
                {
                    idBehavior,
                    hasMutableStats
                }.Concat(behaviors));
            return item;
        }
    }
}