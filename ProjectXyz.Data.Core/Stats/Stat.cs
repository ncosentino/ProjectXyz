using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class Stat : IMutableStat
    {
        #region Constructors
        private Stat(Guid id)
            : this(id, 0)
        {
        }

        private Stat(Guid id, double value)
        {
            this.Id = id;
            this.Value = value;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            set;
        }

        public double Value
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static IMutableStat Create(Guid id)
        {
            Contract.Ensures(Contract.Result<IMutableStat>() != null);
            return new Stat(id);
        }

        public static IMutableStat Create(Guid id, double value)
        {
            Contract.Ensures(Contract.Result<IMutableStat>() != null);
            return new Stat(id, value);
        }
        #endregion
    }
}
