﻿using System;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(ICanUnequip))]
    internal abstract class ICanUnequipItemContract : ICanUnequip
    {
        #region Methods
        public IItem Unequip(Guid slotId)
        {
            Contract.Requires<ArgumentNullException>(slotId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItem>() != null);

            return default(IItem);
        }

        public bool CanUnequip(Guid slotId)
        {
            Contract.Requires<ArgumentNullException>(slotId != Guid.Empty);

            return default(bool);
        }
        #endregion
    }
}
