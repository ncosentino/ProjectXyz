using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IItemContract))]
    public interface IItem : 
        IGameObject, 
        ISocketCandidate, 
        ISocketable, 
        IObservableDurability, 
        IEnchantable
    {
        #region Properties
        string Name { get; }

        string InventoryGraphicResource { get; }

        Guid MagicTypeId { get; }

        string ItemType { get; }

        Guid MaterialTypeId { get; }

        Guid SocketTypeId { get; }

        double Weight { get; }

        double Value { get; }

        IStatCollection Stats { get; }

        IEnumerable<string> EquippableSlots { get; }

        IRequirements Requirements { get; }
        #endregion
    }
}
