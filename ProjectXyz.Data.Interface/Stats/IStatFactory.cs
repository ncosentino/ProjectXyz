using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Stats.Contracts;

namespace ProjectXyz.Data.Interface.Stats
{
    [ContractClass(typeof(IStatFactoryContract))]
    public interface IStatFactory
    {
        #region Methods
        IStat Create(
            Guid id,
            Guid statDefinitionId,
            double value);
        #endregion
    }
}
