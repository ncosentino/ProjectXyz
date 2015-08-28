using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemStatRequirementsRepository
    {
        #region Methods
        IItemStatRequirements Add(
            Guid id,
            Guid itemId,
            Guid statId);

        void RemoveById(Guid id);

        IItemStatRequirements GetById(Guid id);

        IItemStatRequirements GetByItemId(Guid itemId);

        IEnumerable<IItemStatRequirements> GetAll();
        #endregion
    }
}
