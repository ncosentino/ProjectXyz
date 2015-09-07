using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemTypeGeneratorPlugin : IItemTypeGeneratorPlugin
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _magicTypeId;
        private readonly string _itemGeneratorClassName;
        #endregion

        #region Constructors
        private ItemTypeGeneratorPlugin(
            Guid id,
            Guid magicTypeId,
            string itemGeneratorClassName)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(itemGeneratorClassName));
            
            _id = id;
            _magicTypeId = magicTypeId;
            _itemGeneratorClassName = itemGeneratorClassName;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid MagicTypeId { get { return _magicTypeId; } }

        /// <inheritdoc />
        public string ItemGeneratorClassName { get { return _itemGeneratorClassName; } }
        #endregion

        #region Methods
        public static IItemTypeGeneratorPlugin Create(
            Guid id,
            Guid magicTypeId,
            string itemGeneratorClassName)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(itemGeneratorClassName));
            Contract.Ensures(Contract.Result<IItemTypeGeneratorPlugin>() != null);
            
            var itemTypeGeneratorPlugin = new ItemTypeGeneratorPlugin(
                id,
                magicTypeId,
                itemGeneratorClassName);
            return itemTypeGeneratorPlugin;
        }
        #endregion
    }
}
