using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Interface.Stats
{
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
