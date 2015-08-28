using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IEquipment : IItemCollection, IUpdateElapsedTime
    {
        #region Properties
        IItem this[Guid slotId] { get; }
        #endregion
    }
}
