using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IHasStats : IBehavior
    {
        IReadOnlyDictionary<IIdentifier, double> Stats { get; }
    }
}