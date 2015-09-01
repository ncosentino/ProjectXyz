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
            Guid? raceDefinitionId,
            Guid? classDefinitionId);

        void RemoveById(Guid id);

        IItemMiscRequirements GetById(Guid id);
        
        IEnumerable<IItemMiscRequirements> GetAll();
        #endregion
    }
}
