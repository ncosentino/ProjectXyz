using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Core.Items.Requirements
{
    public sealed class ItemDefinitionStatRequirements : IItemDefinitionStatRequirements
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemDefinitionId;
        private readonly Guid _statId;
        #endregion

        #region Constructors
        private ItemDefinitionStatRequirements(
            Guid id,
            Guid itemDefinitionId,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            
            _id = id;
            _itemDefinitionId = itemDefinitionId;
            _statId = statId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid ItemDefinitionId { get { return _itemDefinitionId; } }

        /// <inheritdoc />
        public Guid StatId { get { return _statId; } }
        #endregion

        #region Methods
        public static IItemDefinitionStatRequirements Create(
            Guid id,
            Guid itemDefinitionId,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);

            Contract.Ensures(Contract.Result<IItemDefinitionStatRequirements>() != null);

            var itemStatRequirements = new ItemDefinitionStatRequirements(
                id,
                itemDefinitionId,
                statId);
            return itemStatRequirements;
        }
        #endregion
    }
}