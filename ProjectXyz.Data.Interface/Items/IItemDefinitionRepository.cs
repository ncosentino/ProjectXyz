using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemDefinitionRepository
    {
        #region Methods
        IItemDefinition Add(
            Guid id,
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId);

        void RemoveById(Guid id);

        IItemDefinition GetById(Guid id);

        IEnumerable<IItemDefinition> GetAll();
        #endregion
    }
}
