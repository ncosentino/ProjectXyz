using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements.Requirements;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    [ContractClass(typeof(IItemStatRequirementsContract))]
    public interface IItemStatRequirements
    {
        #region Properties
        Guid Id { get; }

        Guid ItemId { get; }

        Guid StatId { get; }
        #endregion
    }
}
