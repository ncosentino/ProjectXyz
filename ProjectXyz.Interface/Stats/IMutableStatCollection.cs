using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Interface.Stats
{
    public interface IMutableStatCollection<TStat> : IStatCollection<TStat> where TStat : IStat
    {
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
