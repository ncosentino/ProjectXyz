using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemDefinitionStatRepository
    {
        #region Methods
        IItemDefinitionStat Add(
            Guid id,
            Guid itemDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue);

        IItemDefinitionStat GetById(Guid id);
        
        IEnumerable<IItemDefinitionStat> GetByItemDefinitionId(Guid itemDefinitionId);

        IEnumerable<IItemDefinitionStat> GetAll();
        
        void RemoveById(Guid id);
        #endregion
    }
}
