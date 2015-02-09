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
        /// <summary>
        /// Gets the X-coordinate of the <see cref="IActor"/> within the game world
        /// </summary>
        float X { get; }

        /// <summary>
        /// Gets the Y-coordinate of the <see cref="IActor"/> within the game world
        /// </summary>
        float Y { get; }

        /// <summary>
        /// Gets the name of the resource used for animations.
        /// </summary>
        string AnimationResource { get; }

        IEquipment Equipment { get; }

        IInventory Inventory { get; }

        IStatCollection Stats { get; }
        #endregion

        #region Methods
        bool Equip(IItem item);

        bool Unequip(string slot, IMutableInventory destination);

        bool TakeItem(IItem item);

        void UpdatePosition(float x, float y);
        #endregion
    }
}
