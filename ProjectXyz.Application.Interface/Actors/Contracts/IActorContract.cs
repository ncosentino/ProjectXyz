using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Actors.Contracts
{
    [ContractClassFor(typeof(IActor))]
    internal abstract class IActorContract : IActor
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

        public IObservableEquipment Equipment
        {
            get
            {
                Contract.Ensures(Contract.Result<IEquipment>() != null);
                return default(IObservableEquipment);
            }
        }

        public IMutableInventory Inventory
        {
            get
            {
                Contract.Ensures(Contract.Result<IMutableInventory>() != null);
                return default(IMutableInventory);
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
        public abstract bool CanUseItem(IItem item);

        public abstract void UseItem(IItem item);

        public abstract bool CanEquip(IItem item, Guid slotId);

        public abstract void Equip(IItem item, Guid slotId);

        public abstract IItem Unequip(Guid slotId);

        public abstract bool CanUnequip(Guid slotId);

        public abstract void UpdatePosition(float x, float y);

        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
