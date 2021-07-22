using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States
{
    public interface IStateManager : IObservableStateManager
    {
        void SetStates(
            IIdentifier stateTypeId,
            IEnumerable<Tuple<IIdentifier, object>> statePairing);

        void SetState(
            IIdentifier stateTypeId,
            IIdentifier stateId,
            double value);

        void SetState(
            IIdentifier stateTypeId,
            IIdentifier stateId,
            string value);
    }
}
