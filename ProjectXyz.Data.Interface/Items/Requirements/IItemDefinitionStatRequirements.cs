using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemDefinitionStatRequirements
    {
        #region Properties
        Guid Id { get; }

        Guid ItemDefinitionId { get; }

        Guid StatId { get; }
        #endregion
    }
}
