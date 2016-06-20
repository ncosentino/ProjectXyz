using System.Collections.Generic;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Interface.Stats
{
    public interface IStatCollectionFactory
    {
        IStatCollection Create(IEnumerable<IStat> stats);

        IStatCollection Create(IDictionary<IIdentifier, IStat> stats);

        IStatCollection Create(IReadOnlyDictionary<IIdentifier, IStat> stats);
    }
}