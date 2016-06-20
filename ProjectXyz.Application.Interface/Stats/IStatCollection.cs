using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats
{
    public interface IStatCollection : IReadOnlyDictionary<IIdentifier, IStat>
    {
    }
}