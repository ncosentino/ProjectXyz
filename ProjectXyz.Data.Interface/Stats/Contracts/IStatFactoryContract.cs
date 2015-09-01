using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Stats.Contracts
{
    [ContractClassFor(typeof(IStatFactory))]
    public abstract class IStatFactoryContract : IStatFactory
    {
        #region Methods
        public IStat Create(
            Guid id,
            Guid statDefinitionId,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);
           
            Contract.Ensures(Contract.Result<IStat>() != null);

            return default(IStat);
        }
        #endregion
    }
}
