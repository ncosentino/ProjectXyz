using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats.Contracts;

namespace ProjectXyz.Data.Interface.Stats
{
    [ContractClass(typeof(IMutableStatCollectionContract))]
    public interface IMutableStatCollection : IStatCollection
    {
        #region Methods
        new IStat this[string id] { get; set; }

        void Set(IStat stat);
        
        void Add(IEnumerable<IStat> stats);

        bool Remove(IEnumerable<string> ids);

        bool Remove(IEnumerable<IStat> stats);
        #endregion
    }
}
