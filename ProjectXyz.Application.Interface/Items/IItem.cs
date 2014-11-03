using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IItemContract))]
    public interface IItem : IGameObject, ISocketCandidate, ISocketable
    {
        #region Properties
        string Name { get; }

        string MagicType { get; }

        string ItemType { get; }

        string MaterialType { get; }

        double Weight { get; }

        double Value { get; }

        IDurability Durability { get; }

        IEnchantmentCollection Enchantments { get; }

        IRequirements Requirements { get; }
        #endregion

        #region Methods
        void Enchant(IEnumerable<IEnchantment> enchantments);

        void Disenchant(IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
