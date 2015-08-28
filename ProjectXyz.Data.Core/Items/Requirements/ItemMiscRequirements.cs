using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Core.Items.Requirements
{
    public sealed class ItemMiscRequirements : IItemMiscRequirements
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemId;
        private readonly Guid _raceDefinitionId;
        private readonly Guid _classDefinitionId;
        #endregion

        #region Constructors
        private ItemMiscRequirements(
            Guid id,
            Guid itemId,
            Guid raceDefinitionId,
            Guid classDefinitionId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(raceDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(classDefinitionId != Guid.Empty);
            
            _id = id;
            _itemId = itemId;
            _raceDefinitionId = raceDefinitionId;
            _classDefinitionId = classDefinitionId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid ItemId { get { return _itemId; } }

        /// <inheritdoc />
        public Guid RaceDefinitionId { get { return _raceDefinitionId; } }

        /// <inheritdoc />
        public Guid ClassDefinitionId { get { return _classDefinitionId; } }
        #endregion

        #region Methods
        public static IItemMiscRequirements Create(
            Guid id,
            Guid itemId,
            Guid raceDefinitionId,
            Guid classDefinitionId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(raceDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(classDefinitionId != Guid.Empty);

            Contract.Ensures(Contract.Result<IItemMiscRequirements>() != null);

            var itemMiscRequirements = new ItemMiscRequirements(
                id,
                itemId,
                raceDefinitionId,
                classDefinitionId);
            return itemMiscRequirements;
        }
        #endregion
    }
}