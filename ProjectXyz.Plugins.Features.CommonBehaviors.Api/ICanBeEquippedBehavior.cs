using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ICanBeEquippedBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> AllowedEquipSlots { get; }
    }
}