using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Core.Items.Requirements
{
    public sealed class ItemDefinitionItemMiscRequirements : IItemDefinitionItemMiscRequirements
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemDefinitionId;
        private readonly Guid _itemMiscRequirementsId;
        #endregion

        #region Constructors
        private ItemDefinitionItemMiscRequirements(
            Guid id,
            Guid itemDefinitionId,
            Guid itemMiscRequirementsId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(itemMiscRequirementsId != Guid.Empty);
            
            _id = id;
            _itemDefinitionId = itemDefinitionId;
            _itemMiscRequirementsId = itemMiscRequirementsId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid ItemDefinitionId { get { return _itemDefinitionId; } }

        /// <inheritdoc />
        public Guid ItemMiscRequirementsId { get { return _itemMiscRequirementsId; } }
        #endregion

        #region Methods
        public static IItemDefinitionItemMiscRequirements Create(
            Guid id,
            Guid itemDefinitionId,
            Guid itemMiscRequirementsId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(itemMiscRequirementsId != Guid.Empty);

            Contract.Ensures(Contract.Result<IItemDefinitionItemMiscRequirements>() != null);

            var itemDefinitionItemMiscRequirements = new ItemDefinitionItemMiscRequirements(
                id,
                itemDefinitionId,
                itemMiscRequirementsId);
            return itemDefinitionItemMiscRequirements;
        }
        #endregion
    }
}