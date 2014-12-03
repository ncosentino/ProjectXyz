using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class StatFactory : IStatFactory
    {
        #region Constructors
        private StatFactory()
        {
        }
        #endregion

        #region Methods
        public static IStatFactory Create()
        {
            Contract.Ensures(Contract.Result<IStatFactory>() != null);
            return new StatFactory();
        }

        public IStat CreateStat(Guid id, double value)
        {
            return Stat.Create(id, value);
        }
        #endregion
    }
}
