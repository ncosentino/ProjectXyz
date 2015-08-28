using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Items;

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
        #endregion
    }
}
