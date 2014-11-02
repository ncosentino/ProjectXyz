using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public class ReadonlyStatCollection<TStat> : StatCollection<TStat> where TStat : IStat
    {
        #region Constructors
        protected ReadonlyStatCollection(IEnumerable<TStat> stats)
            : base(stats)
        {
        }
        #endregion

        #region Methods
        public static IStatCollection<TStat> Create(IEnumerable<TStat> stats)
        {
            return new ReadonlyStatCollection<TStat>(stats);
        }
        #endregion
    }
}
