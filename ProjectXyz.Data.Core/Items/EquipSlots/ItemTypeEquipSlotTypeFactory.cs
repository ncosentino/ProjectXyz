using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.EquipSlots;

namespace ProjectXyz.Data.Core.Items.EquipSlots
{
    public sealed class ItemTypeEquipSlotTypeFactory : IItemTypeEquipSlotTypeFactory
    {
        #region Constructors
        private ItemTypeEquipSlotTypeFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemTypeEquipSlotTypeFactory Create()
        {
            var factory = new ItemTypeEquipSlotTypeFactory();
            return factory;
        }

        public IItemTypeEquipSlotType Create(
            Guid id,
            Guid itemTypeId,
            Guid equipSlotTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(equipSlotTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemTypeEquipSlotType>() != null);

            var itemTypeEquipSlotType = ItemTypeEquipSlotType.Create(
                id,
                itemTypeId,
                equipSlotTypeId);
            return itemTypeEquipSlotType;
        }
        #endregion
    }
}
