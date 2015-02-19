using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(ICanUseItem))]
    internal abstract class IcanUseItemContract : ICanUseItem
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
