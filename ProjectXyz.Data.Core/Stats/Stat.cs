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
        private Stat(string id)
            : this(id, 0)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
        }

        private Stat(string id, double value)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);

            this.Id = id;
            this.Value = value;
        }
        #endregion

        #region Properties
        public string Id
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
        public static IMutableStat Create(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
            Contract.Ensures(Contract.Result<IMutableStat>() != null);
            return new Stat(id);
        }

        public static IMutableStat Create(string id, double value)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
            Contract.Ensures(Contract.Result<IMutableStat>() != null);
            return new Stat(id, value);
        }
        #endregion
    }
}
