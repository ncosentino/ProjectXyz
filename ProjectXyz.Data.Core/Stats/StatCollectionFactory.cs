using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class StatCollectionFactory : IStatCollectionFactory
    {
        #region Constructors
        private StatCollectionFactory()
        {
        }
        #endregion

        #region Methods
        public static IStatCollectionFactory Create()
        {
            var factory = new StatCollectionFactory();
            return factory;
        }

        public IMutableStatCollection CreateEmpty()
        {
            return Create(Enumerable.Empty<IStat>());
        }

        public IMutableStatCollection Create(IEnumerable<IStat> stats)
        {
            var statCollection = StatCollection.Create(stats);
            return statCollection;
        }
        #endregion
    }
}
