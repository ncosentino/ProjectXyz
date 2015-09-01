using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemDefinitionItemMiscRequirementsRepository
    {
        #region Methods
        IItemDefinitionItemMiscRequirements Add(
            Guid id,
            Guid itemDefinitionId,
            Guid itemMiscRequirementsId);

        void RemoveById(Guid id);

        IItemDefinitionItemMiscRequirements GetById(Guid id);

        IItemDefinitionItemMiscRequirements GetByItemDefinitionId(Guid itemId);

        IEnumerable<IItemDefinitionItemMiscRequirements> GetAll();
        #endregion
    }
}
