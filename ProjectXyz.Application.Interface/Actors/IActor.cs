using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Actors.Contracts;

namespace ProjectXyz.Application.Interface.Actors
{
    [ContractClass(typeof(IActorContract))]
    public interface IActor : IUpdateElapsedTime
    {
        #region Properties
        double MaximumLife { get; }

        double CurrentLife { get; }

        IEquipment Equipment { get; }

        IInventory Inventory { get; }
        #endregion

        #region Methods
        bool Equip(IItem item);

        bool Unequip(string slot, IMutableInventory destination);
        #endregion
    }
}
