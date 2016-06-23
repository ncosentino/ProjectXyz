using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Shared.Stats
{
    public sealed class StatCollectionFactory : IStatCollectionFactory
    {
        public IStatCollection Create(IEnumerable<IStat> stats)
        {
            var dict = (IDictionary<IIdentifier, IStat>)stats.ToDictionary(
                x => x.StatDefinitionId, 
                x => x);
            return Create(dict);
        }

        public IStatCollection Create(IDictionary<IIdentifier, IStat> stats)
        {
            return new StatCollection(stats);
        }

        public IStatCollection Create(IReadOnlyDictionary<IIdentifier, IStat> stats)
        {
            return Create(stats.Select(x => x.Value));
        }
    }
}