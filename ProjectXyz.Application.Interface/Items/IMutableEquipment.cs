using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IMutableEquipment : IObservableEquipment, INotifyCollectionChanged, ICanEquip, ICanUnequip
    {
    }
}
