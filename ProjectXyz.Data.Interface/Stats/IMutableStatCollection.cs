using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats.Contracts;

namespace ProjectXyz.Data.Interface.Stats
{
    [ContractClass(typeof(IMutableStatCollectionContract))]
    public interface IMutableStatCollection : IStatCollection
    {
        #region Methods
        new IStat this[Guid id] { get; set; }

        void Set(IStat stat);
        
        void Add(IEnumerable<IStat> stats);

        bool Remove(IEnumerable<Guid> ids);

        bool Remove(IEnumerable<IStat> stats);
        #endregion
    }
}
