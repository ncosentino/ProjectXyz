using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IMutableEquipment : IEquipment, INotifyCollectionChanged
    {
        #region Methods
        bool Equip(IItem item, string slot);

        IItem Unequip(string slot);
        #endregion
    }
}
