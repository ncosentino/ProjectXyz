using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketPatternDefinitionRepository
    {
        #region Methods
        ISocketPatternDefinition Add(
            Guid id,
            Guid nameStringResourceId,
            Guid? inventoryGraphicResourceId,
            Guid? magicTypeId,
            float chance);

        void RemoveById(Guid id);

        ISocketPatternDefinition GetById(Guid id);

        IEnumerable<ISocketPatternDefinition> GetAll();
        #endregion
    }
}