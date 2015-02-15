using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Contracts;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Items
{
    [ContractClass(typeof(IItemStoreContract))]
    public interface IItemStore : IGameObject
    {
        #region Properties
        string Name { get; }

        string InventoryGraphicResource { get; }

        Guid MagicTypeId { get; }

        string ItemType { get; }

        Guid MaterialTypeId { get; }

        IMutableStatCollection Stats { get; }

        IMutableEnchantmentCollection Enchantments { get; }

        IRequirements Requirements { get; }

        IMutableItemStoreCollection SocketedItems { get; }

        IEnumerable<string> EquippableSlots { get; }
        #endregion
    }
}
