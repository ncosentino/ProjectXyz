using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemMiscRequirementsRepository
    {
        #region Methods
        IItemMiscRequirements Add(
            Guid id,
            Guid itemId,
            Guid raceDefinitionId,
            Guid classDefinitionId);

        void RemoveById(Guid id);

        IItemMiscRequirements GetById(Guid id);

        IItemMiscRequirements GetByItemId(Guid itemId);

        IEnumerable<IItemMiscRequirements> GetAll();
        #endregion
    }
}
