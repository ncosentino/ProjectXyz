using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.GameObjects.Items;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IMutableEquipment : 
        IObservableEquipment, 
        INotifyCollectionChanged,
        ICanEquip, 
        ICanUnequip
    {
    }
}
