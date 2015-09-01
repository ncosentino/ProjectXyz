using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements.Contracts
{
    [ContractClassFor(typeof(IItemMiscRequirements))]
    public abstract class IItemMiscRequirementsContract : IItemMiscRequirements
    {
        #region Properties
        public Guid Id
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid? RaceDefinitionId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid?>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid? ClassDefinitionId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid?>() != Guid.Empty);
                return default(Guid);
            }
        }
        #endregion
    }
}
