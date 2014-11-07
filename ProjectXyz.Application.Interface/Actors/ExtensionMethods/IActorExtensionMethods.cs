using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Application.Interface.Actors.ExtensionMethods
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
        #endregion
    }
}
