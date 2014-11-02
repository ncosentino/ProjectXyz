using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Stats.Contracts;

namespace ProjectXyz.Interface.Stats
{
    [ContractClass(typeof(IStatCollectionContract<>))]
    public interface IStatCollection<TStat> : IEnumerable<TStat> where TStat : IStat
    {
        #region Properties
        int Count { get; }

        TStat this[string id] { get; }
        #endregion

        #region Methods
        bool Contains(string id);
        #endregion
    }
}
