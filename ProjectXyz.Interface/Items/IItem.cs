using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Interface.Enchantments;

namespace ProjectXyz.Interface.Items
{
    [ContractClass(typeof(IItemContract))]
    public interface IItem : IGameObject
    {
        #region Properties
        string Name { get; }

        string MagicType { get; }

        double Weight { get; }

        double Value { get; }

        IDurability Durability { get; }

        IEnchantmentCollection Enchantments { get; }

        IRequirements Requirements { get; }

        IInventory Sockets { get; }
        #endregion

        #region Methods
        void Enchant(IEnumerable<IEnchantment> enchantments);

        void Disenchant(IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
