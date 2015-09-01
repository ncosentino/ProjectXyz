using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemDefinitionFactory : IItemDefinitionFactory
    {
        #region Constructors
        private ItemDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemDefinitionFactory Create()
        {
            var factory = new ItemDefinitionFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemDefinition Create(
            Guid id,
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId)
        {
            var itemDefinition = ItemDefinition.Create(
                id,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);
            return itemDefinition;
        }
        #endregion
    }
}
