using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Interface.Items
{
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
