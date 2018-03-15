using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Behaviors
{
    public interface IHasMutableStats : IHasStats
    {
        void MutateStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}