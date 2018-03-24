using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Behaviors
{
    public interface IHasMutableStatsBehavior : IHasStatsBehavior
    {
        void MutateStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}