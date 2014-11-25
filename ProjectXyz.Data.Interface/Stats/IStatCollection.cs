using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Stats.Contracts;

namespace ProjectXyz.Data.Interface.Stats
{
    [ContractClass(typeof(IStatCollectionContract))]
    public interface IStatCollection : IEnumerable<IStat>
    {
        #region Properties
        int Count { get; }

        IStat this[Guid id] { get; }
        #endregion

        #region Methods
        bool Contains(Guid id);
        #endregion
    }
}
