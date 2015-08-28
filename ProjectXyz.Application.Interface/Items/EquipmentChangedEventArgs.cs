using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items
{
    public class EquipmentChangedEventArgs : EventArgs
    {
        #region Fields
        private readonly Guid _slotId;
        #endregion

        #region Constructors
        public EquipmentChangedEventArgs(Guid slotId)
        {
            Contract.Requires<ArgumentException>(slotId != Guid.Empty);

            _slotId = slotId;
        }
        #endregion

        #region Properties
        public Guid SlotId
        {
            get { return _slotId; }
        }
        #endregion

        #region Methods
        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_slotId != Guid.Empty);
        }
        #endregion
    }
}
