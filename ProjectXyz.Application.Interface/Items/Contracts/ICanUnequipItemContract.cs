using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(ICanUnequip))]
    internal abstract class ICanUnequipItemContract : ICanUnequip
    {
        #region Methods
        public IItem Unequip(string slot)
        {
            Contract.Requires<ArgumentNullException>(slot != null);
            Contract.Requires<ArgumentException>(slot.Trim().Length > 0);
            Contract.Ensures(Contract.Result<IItem>() != null);

            return default(IItem);
        }

        public bool CanUnequip(string slot)
        {
            Contract.Requires<ArgumentNullException>(slot != null);
            Contract.Requires<ArgumentException>(slot.Trim().Length > 0);

            return default(bool);
        }
        #endregion
    }
}
