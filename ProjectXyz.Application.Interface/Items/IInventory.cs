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
    [ContractClass(typeof(IInventoryContract))]
    public interface IInventory : IUpdateElapsedTime
    {
        #region Properties
        double CurrentWeight { get; }

        double WeightCapacity { get; }

        int ItemCapacity { get; }

        IItemCollection Items { get; }
        #endregion
    }
}
