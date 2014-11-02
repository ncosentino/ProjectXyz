using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Interface.Items
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
