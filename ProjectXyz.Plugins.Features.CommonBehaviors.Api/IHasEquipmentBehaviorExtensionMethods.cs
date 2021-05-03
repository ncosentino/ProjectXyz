using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public static class IHasEquipmentBehaviorExtensionMethods
    {
        public static IEnumerable<ICanBeEquippedBehavior> GetCanBeEquippedBehaviors(this IHasEquipmentBehavior hasEquipmentBehavior)
        {
            return hasEquipmentBehavior
                .GetCanBeEquippedBehaviorsBySlot()
                .Select(x => x.Item2);
        }

        public static IEnumerable<Tuple<IIdentifier, ICanBeEquippedBehavior>> GetCanBeEquippedBehaviorsBySlot(this IHasEquipmentBehavior hasEquipmentBehavior)
        {
            foreach (var equipSlotId in hasEquipmentBehavior.SupportedEquipSlotIds)
            {
                if (!hasEquipmentBehavior.TryGet(
                    equipSlotId,
                    out var canBeEquippedBehavior))
                {
                    continue;
                }

                yield return Tuple.Create(
                    equipSlotId,
                    canBeEquippedBehavior);
            }
        }

        public static IEnumerable<Tuple<IIdentifier, IGameObject>> GetEquippedItemsBySlot(this IHasEquipmentBehavior hasEquipmentBehavior)
        {
            return hasEquipmentBehavior
                .GetCanBeEquippedBehaviorsBySlot()
                .Select(x => Tuple.Create(
                    x.Item1,
                    x.Item2.Owner));
        }

        public static IEnumerable<IGameObject> GetEquippedItems(this IHasEquipmentBehavior hasEquipmentBehavior)
        {
            return hasEquipmentBehavior
                .GetCanBeEquippedBehaviors()
                .Select(x => x.Owner);
        }
    }
}