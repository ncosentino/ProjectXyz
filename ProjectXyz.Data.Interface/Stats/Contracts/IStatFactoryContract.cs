using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Stats.Contracts
{
    [ContractClassFor(typeof(IStatFactory))]
    public abstract class IStatFactoryContract : IStatFactory
    {
        #region Methods
        public IStat CreateStat(Guid id, double value)
        {
            Contract.Ensures(Contract.Result<IStat>() != null);

            return default(IStat);
        }
        #endregion
    }
}
