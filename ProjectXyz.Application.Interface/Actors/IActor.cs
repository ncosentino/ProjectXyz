using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Actors.Contracts;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Actors
{
    [ContractClass(typeof(IActorContract))]
    public interface IActor : IUpdateElapsedTime
    {
        #region Properties
        IEquipment Equipment { get; }

        IInventory Inventory { get; }

        IStatCollection Stats { get; }
        #endregion

        #region Methods
        bool Equip(IItem item);

        bool Unequip(string slot, IMutableInventory destination);

        bool TakeItem(IItem item);
        #endregion
    }
}
