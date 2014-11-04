using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class ReadonlyStat : Stat
    {
        #region Constructors
        private ReadonlyStat(string id, double value)
            : base(id, value)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
        }
        #endregion

        #region Methods
        public static IStat Create(string id, double value)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
            Contract.Ensures(Contract.Result<IStat>() != null);
            return new ReadonlyStat(id, value);
        }
        #endregion
    }
}
