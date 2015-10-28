using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Data.Core.Items.Names
{
    public sealed class ItemNameAffix : IItemNameAffix
    {
        #region Fields
        private readonly Guid _id;
        private readonly bool _isPrefix;
        private readonly Guid _itemTypeGroupingId;
        private readonly Guid _nameStringResourceId;
        private readonly Guid _magicTypeGroupingId;
        #endregion

        #region Constructors
        private ItemNameAffix(
            Guid id,
            bool isPrefix,
            Guid itemTypeGroupingId,
            Guid magicTypeGroupingId,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeGroupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeGroupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);

            _id = id;
            _isPrefix = isPrefix;
            _itemTypeGroupingId = itemTypeGroupingId;
            _nameStringResourceId = nameStringResourceId;
            _magicTypeGroupingId = magicTypeGroupingId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public bool IsPrefix { get { return _isPrefix; } }

        /// <inheritdoc />
        public Guid ItemTypeGroupingId { get { return _itemTypeGroupingId; } }

        /// <inheritdoc />
        public Guid NameStringResourceId { get { return _nameStringResourceId; } }

        /// <inheritdoc />
        public Guid MagicTypeGroupingId { get { return _magicTypeGroupingId; } }
        #endregion

        #region Methods
        public static IItemNameAffix Create(
            Guid id,
            bool isPrefix,
            Guid itemTypeGroupingId,
            Guid nameStringResourceId,
            Guid magicTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeGroupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeGroupingId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemNameAffix>() != null);
            
            var itemNameAffix = new ItemNameAffix(
                id,
                isPrefix,
                itemTypeGroupingId,
                nameStringResourceId,
                magicTypeGroupingId);
            return itemNameAffix;
        }
        #endregion
    }
}
