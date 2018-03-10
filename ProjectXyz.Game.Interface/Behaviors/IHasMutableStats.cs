using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IHasMutableStats : IHasStats
    {
        void MutateStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}