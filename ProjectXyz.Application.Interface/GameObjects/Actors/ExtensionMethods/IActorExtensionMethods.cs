using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Application.Interface.GameObjects.Actors.ExtensionMethods
{
    public static class IActorExtensionMethods
    {
        #region Methods
        public static bool HasItemEquipped(this IActor actor, IItem item)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            Contract.Requires<ArgumentNullException>(item != null);
            return actor.Equipment.HasItemEquipped(item);
        }

        public static bool IsOverburderned(this IActor actor)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            return actor.Inventory.IsOverburderned();
        }
        #endregion
    }
}
