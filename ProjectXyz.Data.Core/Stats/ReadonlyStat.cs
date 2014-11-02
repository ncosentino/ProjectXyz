using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class ReadonlyStat : Stat
    {
        #region Constructors
        private ReadonlyStat(string id, double value)
            : base(id, value)
        {
        }
        #endregion

        #region Methods
        public static IStat Create(string id, double value)
        {
            return new ReadonlyStat(id, value);
        }
        #endregion
    }
}
