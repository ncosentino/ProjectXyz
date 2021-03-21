using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class EquipmentGameObjectsForBehavior : IDiscoverableGameObjectsForBehavior
    {
        public Type SupportedBehaviorType { get; } = typeof(CanEquipBehavior);

        public IEnumerable<IGameObject> GetChildren(IBehavior behavior)
        {
            var hasEquipmentBehavior = (IHasEquipmentBehavior)behavior;
            foreach (var item in hasEquipmentBehavior.GetEquippedItems())
            {
                yield return item;
            }
        }
    }
}
