using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items
{
    public class EquipmentChangedEventArgs : EventArgs
    {
        #region Fields
        private readonly string _slot;
        #endregion

        #region Constructors
        public EquipmentChangedEventArgs(string slot)
        {
            Contract.Requires<ArgumentNullException>(slot != null);
            Contract.Requires<ArgumentException>(slot.Trim().Length > 0);

            _slot = slot.Trim();
        }
        #endregion

        #region Properties
        public string Slot
        {
            get { return _slot; }
        }
        #endregion

        #region Methods
        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(!string.IsNullOrEmpty(_slot));
        }
        #endregion
    }
}
