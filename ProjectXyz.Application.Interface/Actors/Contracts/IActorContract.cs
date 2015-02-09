﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Actors.Contracts
{
    [ContractClassFor(typeof(IActor))]
    public abstract class IActorContract : IActor
    {
        #region Properties
        public abstract float X { get; }

        public abstract float Y { get; }

        public string AnimationResource
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>().Trim().Length > 0);

                return default(string);
            }
        }

        public IEquipment Equipment
        {
            get
            {
                Contract.Ensures(Contract.Result<IEquipment>() != null);
                return default(IEquipment);
            }
        }

        public IInventory Inventory
        {
            get
            {
                Contract.Ensures(Contract.Result<IInventory>() != null);
                return default(IInventory);
            }
        }

        public IStatCollection Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatCollection>() != null);
                return default(IStatCollection);
            }
        }
        #endregion

        #region Methods
        public bool Equip(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return default(bool);
        }

        public bool Unequip(string slot, IMutableInventory destination)
        {
            Contract.Requires<ArgumentNullException>(slot != null);
            Contract.Requires<ArgumentException>(slot != string.Empty);
            Contract.Requires<ArgumentNullException>(destination != null);
            return default(bool);
        }

        public bool TakeItem(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return default(bool);
        }

        public abstract void UpdatePosition(float x, float y);

        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
