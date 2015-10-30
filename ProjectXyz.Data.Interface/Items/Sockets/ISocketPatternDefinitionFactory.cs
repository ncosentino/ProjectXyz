using System;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketPatternDefinitionFactory
    {
        #region Methods
        ISocketPatternDefinition Create(
            Guid id,
            Guid nameStringResourceId,
            Guid? inventoryGraphicResourceId,
            Guid? magicTypeId,
            float chance);
        #endregion
    }
}