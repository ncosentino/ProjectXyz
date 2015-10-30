using System;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketPatternDefinition
    {
        #region Properties
        Guid Id { get; }

        float Chance { get; }

        Guid? InventoryGraphicResourceId { get; }

        Guid? MagicTypeId { get; }

        Guid NameStringResourceId { get; }
        #endregion
    }
}