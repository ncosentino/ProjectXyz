using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemDefinitionStatRequirementsRepository
    {
        #region Methods
        IItemDefinitionStatRequirements Add(
            Guid id,
            Guid itemDefinitionId,
            Guid statId);

        void RemoveById(Guid id);

        IItemDefinitionStatRequirements GetById(Guid id);

        IEnumerable<IItemDefinitionStatRequirements> GetByItemDefinitionId(Guid itemDefinitionId);

        IEnumerable<IItemDefinitionStatRequirements> GetAll();
        #endregion
    }
}
