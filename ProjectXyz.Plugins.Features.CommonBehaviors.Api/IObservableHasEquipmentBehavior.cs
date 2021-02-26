using System;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IObservableHasEquipmentBehavior : IHasEquipmentBehavior
    {
        event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>>> Equipped;

        event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>>> Unequipped;
    }
}