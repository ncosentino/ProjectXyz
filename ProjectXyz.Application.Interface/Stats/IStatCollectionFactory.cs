using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats
{
    public interface IStatCollectionFactory
    {
        IStatCollection Create(IEnumerable<IStat> stats);

        IStatCollection Create(IDictionary<IIdentifier, IStat> stats);

        IStatCollection Create(IReadOnlyDictionary<IIdentifier, IStat> stats);
    }
}