using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IInventory : IUpdateElapsedTime
    {
        #region Properties
        int CurrentWeight { get; }

        int WeightCapacity { get; }

        int ItemCapacity { get; }

        IItemCollection Items { get; }
        #endregion
    }
}
