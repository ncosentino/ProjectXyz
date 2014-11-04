using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class MutableStat : Stat, IMutableStat
    {
        #region Constructors
        private MutableStat(string id)
            : this(id, 0)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
        }

        private MutableStat(string id, double value)
            : base(id, value)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
        }
        #endregion

        #region Methods
        public static IMutableStat Create(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
            Contract.Ensures(Contract.Result<IMutableStat>() != null);
            return new MutableStat(id);
        }

        public static IMutableStat Create(string id, double value)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
            Contract.Ensures(Contract.Result<IMutableStat>() != null);
            return new MutableStat(id, value);
        }

        public void SetValue(double value)
        {
            this.Value = value;
        }
        #endregion
    }
}
