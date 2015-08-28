using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.EquipSlots;

namespace ProjectXyz.Data.Core.Items.EquipSlots
{
    public sealed class EquipSlotType : IEquipSlotType
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _nameStringResourceId;
        #endregion

        #region Constructors
        private EquipSlotType(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            
            _id = id;
            _nameStringResourceId = nameStringResourceId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid NameStringResourceId { get { return _nameStringResourceId; } }
        #endregion

        #region Methods
        public static IEquipSlotType Create(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEquipSlotType>() != null);
            
            var equipSlotType = new EquipSlotType(
                id,
                nameStringResourceId);
            return equipSlotType;
        }
        #endregion
    }
}
