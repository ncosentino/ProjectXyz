using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasMutableStatsBehavior : IHasStatsBehavior
    {
        void MutateStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}