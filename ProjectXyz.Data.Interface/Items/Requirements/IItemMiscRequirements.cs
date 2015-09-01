using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements.Contracts;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    [ContractClass(typeof(IItemMiscRequirementsContract))]
    public interface IItemMiscRequirements
    {
        #region Properties
        Guid Id { get; }
        
        Guid? RaceDefinitionId { get; }

        Guid? ClassDefinitionId { get; }
        #endregion
    }
}
