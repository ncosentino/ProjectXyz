using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class MutableStat : Stat, IMutableStat
    {
        #region Constructors
        private MutableStat(string id)
            : this(id, 0)
        {
        }

        private MutableStat(string id, double value)
            : base(id, value)
        {
        }
        #endregion

        #region Methods
        public static IMutableStat Create(string id)
        {
            return new MutableStat(id);
        }

        public static IMutableStat Create(string id, double value)
        {
            return new MutableStat(id, value);
        }

        public void SetValue(double value)
        {
            this.Value = value;
        }
        #endregion
    }
}
