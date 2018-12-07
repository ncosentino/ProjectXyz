using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class CanBeEquippedBehavior :
        BaseBehavior,
        ICanBeEquippedBehavior
    {
        public CanBeEquippedBehavior(IEnumerable<IIdentifier> allowedEquipSlots)
        {
            AllowedEquipSlots = allowedEquipSlots.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> AllowedEquipSlots { get; }
    }
}
