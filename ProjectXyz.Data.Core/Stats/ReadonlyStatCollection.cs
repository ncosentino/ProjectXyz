using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public class ReadonlyStatCollection<TStat> : StatCollection<TStat> where TStat : IStat
    {
        #region Constructors
        protected ReadonlyStatCollection(IEnumerable<TStat> stats)
            : base(stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
        }
        #endregion

        #region Methods
        public static IStatCollection<TStat> Create(IEnumerable<TStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<IStatCollection<TStat>>() != null);
            return new ReadonlyStatCollection<TStat>(stats);
        }
        #endregion
    }
}
