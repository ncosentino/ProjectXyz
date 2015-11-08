using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.GameObjects.Actors;
using ProjectXyz.Application.Interface.Interactions;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Application.Core.GameObjects.Loot
{
    public class LootDrop : IMutableLootDrop
    {
        #region Fields
        private readonly IItem _item;
        private readonly IInteraction _pickupLootInteraction;
        #endregion

        #region Constructors
        private LootDrop(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);

            _item = item;

            _pickupLootInteraction = PickupLootInteraction.Create(this);
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> ItemBeingTaken;
        #endregion

        #region Methods
        public static IMutableLootDrop Create(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Ensures(Contract.Result<IMutableLootDrop>() != null);

            return new LootDrop(item);
        }

        public IEnumerable<IInteraction> GetPossibleInteractions(IInteractionContext interactionContext, IActor actor)
        {
            if (_pickupLootInteraction.CanInteract(interactionContext, actor))
            {
                yield return _pickupLootInteraction;
            }
        }

        public IItem TakeItem()
        {
            var handler = ItemBeingTaken;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }

            return _item;
        }
        #endregion
    }

    public interface ILootDrop
    {
    }

    public interface IObserableLootDrop : ILootDrop
    {
        #region Events
        event EventHandler<EventArgs> ItemBeingTaken;
        #endregion
    }

    public interface IMutableLootDrop : IInteractable, IObserableLootDrop
    {
        #region Methods
        IItem TakeItem();
        #endregion   
    }

    public class PickupLootInteraction : IInteraction
    {
        #region Fields
        private readonly IMutableLootDrop _mutableLootDrop;
        #endregion

        #region Constructors
        private PickupLootInteraction(IMutableLootDrop mutableLootDrop)
        {
            _mutableLootDrop = mutableLootDrop;
        }
        #endregion

        #region Methods
        public static IInteraction Create(IMutableLootDrop mutableLootDrop)
        {
            return new PickupLootInteraction(mutableLootDrop);
        }

        public void Interact(IInteractionContext interactionContext, IActor actor)
        {
            actor.Inventory.Add(_mutableLootDrop.TakeItem());
        }

        public bool CanInteract(IInteractionContext interactionContext, IActor actor)
        {
            return true;
        }
        #endregion
    }
}
