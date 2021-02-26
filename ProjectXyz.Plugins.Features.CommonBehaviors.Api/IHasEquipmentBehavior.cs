using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasEquipmentBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> SupportedEquipSlotIds { get; }

        bool TryGet(
            IIdentifier equipSlotId,
            out ICanBeEquippedBehavior canBeEquipped);
    }
}