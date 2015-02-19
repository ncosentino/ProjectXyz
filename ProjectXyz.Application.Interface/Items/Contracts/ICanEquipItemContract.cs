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
        public bool CanEquip(IItem item, string slot)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentNullException>(slot != null);
            Contract.Requires<ArgumentException>(slot.Trim().Length > 0);
            
            return default(bool);
        }

        public void Equip(IItem item, string slot)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentNullException>(slot != null);
            Contract.Requires<ArgumentException>(slot.Trim().Length > 0);
        }
        #endregion
    }
}
