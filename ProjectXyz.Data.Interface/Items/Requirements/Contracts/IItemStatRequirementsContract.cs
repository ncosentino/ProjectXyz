using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements.Requirements
{
    [ContractClassFor(typeof(IItemStatRequirements))]
    public abstract class IItemStatRequirementsContract : IItemStatRequirements
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

        public Guid ItemId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid StatId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }
        #endregion
    }
}
