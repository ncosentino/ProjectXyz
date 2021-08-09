using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasStatsBehavior : IHasObservableStatsBehavior
    {
        void MutateStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}