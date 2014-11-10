using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IMutableInventoryContract))]
    public interface IMutableInventory : IInventory
    {
        #region Properties
        new double WeightCapacity { get; set; }

        new int ItemCapacity { get; set; }
        #endregion

        #region Methods
        void AddItem(IItem item);

        void RemoveItem(IItem item);
        #endregion
    }
}
