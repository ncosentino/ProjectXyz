using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Items.Requirements.Contracts;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Items.Requirements
{
    [ContractClass(typeof(IItemRequirementsContract))]
    public interface IItemRequirements
    {
        #region Properties
        Guid RaceDefinitionId { get; }

        Guid ClassDefinitionId { get; }

        IStatCollection Stats { get; }
        #endregion
    }
}
