using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Items.Requirements.Contracts
{
    [ContractClassFor(typeof(IItemRequirements))]
    public abstract class IItemRequirementsContract : IItemRequirements
    {
        #region Properties
        public Guid RaceDefinitionId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid ClassDefinitionId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public IStatCollection Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatCollection>() != null);
                return default(IStatCollection);
            }
        }
        #endregion
    }
}
