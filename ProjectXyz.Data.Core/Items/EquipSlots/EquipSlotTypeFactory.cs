using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.EquipSlots;

namespace ProjectXyz.Data.Core.Items.EquipSlots
{
    public sealed class EquipSlotTypeFactory : IEquipSlotTypeFactory
    {
        #region Constructors
        private EquipSlotTypeFactory()
        {
        }
        #endregion

        #region Methods
        public static IEquipSlotTypeFactory Create()
        {
            var factory = new EquipSlotTypeFactory();
            return factory;
        }

        public IEquipSlotType Create(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEquipSlotType>() != null);
            
            var equipSlotType = EquipSlotType.Create(
                id,
                nameStringResourceId);
            return equipSlotType;
        }
        #endregion
    }
}
