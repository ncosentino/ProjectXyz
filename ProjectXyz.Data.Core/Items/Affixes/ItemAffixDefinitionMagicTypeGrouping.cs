using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Core.Items.Affixes
{
    public sealed class ItemAffixDefinitionMagicTypeGrouping : IItemAffixDefinitionMagicTypeGrouping
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemAffixDefinitionId;
        private readonly Guid _magicTypeGroupingId;
        #endregion

        #region Constructors
        private ItemAffixDefinitionMagicTypeGrouping(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid magicTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemAffixDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeGroupingId != Guid.Empty);

            _id = id;
            _itemAffixDefinitionId = itemAffixDefinitionId;
            _magicTypeGroupingId = magicTypeGroupingId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid ItemAffixDefinitionId { get { return _itemAffixDefinitionId; } }

        /// <inheritdoc />
        public Guid MagicTypeGroupingId { get { return _magicTypeGroupingId; } }
        #endregion

        #region Methods
        public static IItemAffixDefinitionMagicTypeGrouping Create(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid magicTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemAffixDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeGroupingId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionMagicTypeGrouping>() != null);

            var itemAffixDefinitionEnchantment = new ItemAffixDefinitionMagicTypeGrouping(
                id,
                itemAffixDefinitionId,
                magicTypeGroupingId);
            return itemAffixDefinitionEnchantment;
        }
        #endregion
    }
}
