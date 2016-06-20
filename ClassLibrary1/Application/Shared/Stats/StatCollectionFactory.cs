using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClassLibrary1.Application.Interface.Stats;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Shared.Stats
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

        private sealed class StatCollection : 
            ReadOnlyDictionary<IIdentifier, IStat>,
            IStatCollection
        {
            public StatCollection(IDictionary<IIdentifier, IStat> dictionary) 
                : base(dictionary)
            {
            }
        }
    }
}