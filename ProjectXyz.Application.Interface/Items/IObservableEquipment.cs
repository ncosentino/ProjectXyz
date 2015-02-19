using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IObservableEquipment : IEquipment
    {
        #region Events
        event EventHandler<EquipmentChangedEventArgs> EquipmentChanged;
        #endregion
    }
}
