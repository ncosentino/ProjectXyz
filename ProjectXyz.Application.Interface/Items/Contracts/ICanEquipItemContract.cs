using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(ICanEquip))]
    internal abstract class ICanEquipItemContract : ICanEquip
    {
        #region Methods
        public bool CanEquip(IItem item, Guid slotId)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentException>(slotId != Guid.Empty);
            
            return default(bool);
        }

        public void Equip(IItem item, Guid slotId)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentException>(slotId != Guid.Empty);
        }
        #endregion
    }
}
