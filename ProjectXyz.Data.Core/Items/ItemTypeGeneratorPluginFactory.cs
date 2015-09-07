using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemTypeGeneratorPluginFactory : IItemTypeGeneratorPluginFactory
    {
        #region Constructors
        private ItemTypeGeneratorPluginFactory()
        {
            
        }
        #endregion

        #region Methods
        public static IItemTypeGeneratorPluginFactory Create()
        {
            var factory = new ItemTypeGeneratorPluginFactory();
            return factory;
        }

        public IItemTypeGeneratorPlugin Create(
            Guid id,
            Guid magicTypeId,
            string itemGeneratorClassName)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(itemGeneratorClassName));
            Contract.Ensures(Contract.Result<IItemTypeGeneratorPlugin>() != null);
            
            var itemTypeGeneratorPlugin = ItemTypeGeneratorPlugin.Create(
                id,
                magicTypeId,
                itemGeneratorClassName);
            return itemTypeGeneratorPlugin;
        }
        #endregion
    }
}
