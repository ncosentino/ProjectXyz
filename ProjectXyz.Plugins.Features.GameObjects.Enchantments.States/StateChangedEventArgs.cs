using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States
{
    public sealed class StateChangedEventArgs : EventArgs
    {
        public StateChangedEventArgs(
            IIdentifier stateTypeId,
            IReadOnlyDictionary<IIdentifier, Tuple<object, object>> changedStates)
        {
            StateTypeId = stateTypeId;
            ChangedStates = changedStates;
        }

        public IIdentifier StateTypeId { get; }

        public IReadOnlyDictionary<IIdentifier, Tuple<object, object>> ChangedStates { get; }
    }
}
