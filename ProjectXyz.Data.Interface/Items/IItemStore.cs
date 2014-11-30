using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        string Name { get; set; }

        Guid MagicTypeId { get; set; }

        string ItemType { get; set; }

        string MaterialType { get; set; }

        IMutableStatCollection Stats { get; }

        IMutableEnchantmentCollection Enchantments { get; }

        IRequirements Requirements { get; }

        IMutableItemStoreCollection SocketedItems { get; }

        IList<string> EquippableSlots { get; }
        #endregion
    }
}
