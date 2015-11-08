using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.GameObjects.Items;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(ICanUseItem))]
    internal abstract class ICanUseItemContract : ICanUseItem
    {
        #region Methods
        public bool CanUseItem(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return default(bool);
        }

        public void UseItem(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
        }
        #endregion
    }
}
