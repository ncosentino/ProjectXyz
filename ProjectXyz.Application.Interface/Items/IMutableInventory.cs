using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IMutableInventoryContract))]
    public interface IMutableInventory : IInventory, IMutableItemCollection
    {
        #region Properties
        new double WeightCapacity { get; set; }

        new int ItemCapacity { get; set; }
        #endregion

        #region Methods
        bool SlotOccupied(int slot);

        void Add(IItem item, int slot);
        #endregion
    }
}
