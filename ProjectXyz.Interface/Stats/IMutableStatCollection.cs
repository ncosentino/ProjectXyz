using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Interface.Stats.Contracts;

namespace ProjectXyz.Interface.Stats
{
    [ContractClass(typeof(IMutableStatCollectionContract<>))]
    public interface IMutableStatCollection<TStat> : IStatCollection<TStat> where TStat : IStat
    {
        #region Properties
        new TStat this[string statId] { get; set; }
        #endregion

        #region Methods
        void Set(TStat stat);
        
        void Add(TStat stat);

        void AddRange(IEnumerable<TStat> stats);

        void Remove(string id);

        void Remove(TStat stat);

        void RemoveRange(IEnumerable<string> ids);

        void RemoveRange(IEnumerable<TStat> stats);
        #endregion
    }
}
