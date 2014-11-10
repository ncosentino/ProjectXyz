using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Items.Materials;
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

        IMaterial Material { get; }

        double Weight { get; }

        double Value { get; }

        IStatCollection Stats { get; }

        IEnumerable<string> EquippableSlots { get; }

        IDurability Durability { get; }

        IEnchantmentCollection Enchantments { get; }

        IRequirements Requirements { get; }
        #endregion

        #region Events
        event EventHandler<EventArgs> Broken;
        #endregion

        #region Methods
        void Enchant(IEnumerable<IEnchantment> enchantments);

        void Disenchant(IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
