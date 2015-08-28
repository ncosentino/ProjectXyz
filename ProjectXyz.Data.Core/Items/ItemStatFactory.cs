using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemStatFactory : IItemStatFactory
    {
        #region Constructors
        private ItemStatFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemStatFactory Create()
        {
            var factory = new ItemStatFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemStat Create(
            Guid id,
            Guid itemId,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemStat>() != null);
            
            var itemStat = ItemStat.Create(
                id,
                itemId,
                statId);
            return itemStat;
        }
        #endregion
    }
}
