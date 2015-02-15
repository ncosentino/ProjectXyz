using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Core.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemManager : IItemManager
    {
        #region Fields
        #endregion

        #region Constructors
        private ItemManager()
        {
        }
        #endregion

        #region Methods
        public static IItemManager Create()
        {
            Contract.Ensures(Contract.Result<IItemManager>() != null);

            return new ItemManager();
        }

        public IItem GetItemById(Guid itemId, IItemContext itemContext)
        {
            var item = ItemStore.Create(
                Guid.NewGuid(),
                "Awesome Item",
                "Graphics/Items/Gloves/Leather Gloves",
                "Gloves",
                Guid.NewGuid(),
                new[] { "Gloves" });

            return Item.Create(itemContext, item);
        }
        #endregion
    }
}
