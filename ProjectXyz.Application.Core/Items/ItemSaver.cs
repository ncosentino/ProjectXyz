using System;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;

namespace ProjectXyz.Application.Core.Items
{
    public class ItemSaver : IItemSaver
    {
        #region Fields
        private readonly IEnchantmentSaver _enchantmentSaver;
        #endregion

        #region Constructors
        private ItemSaver(IEnchantmentSaver enchantmentSaver)
        {
            Contract.Requires<ArgumentNullException>(enchantmentSaver != null);
            _enchantmentSaver = enchantmentSaver;
        }
        #endregion

        #region Methods
        public static IItemSaver Create(IEnchantmentSaver enchantmentSaver)
        {
            Contract.Requires<ArgumentNullException>(enchantmentSaver != null);
            Contract.Ensures(Contract.Result<IItemSaver>() != null);

            return new ItemSaver(enchantmentSaver);
        }

        public ProjectXyz.Data.Interface.Items.IItem Save(IItem source)
        {
            var destination = ProjectXyz.Data.Core.Items.Item.Create();

            destination.Id = source.Id;
            destination.Name = source.ItemType;
            destination.MagicTypeId = source.MagicTypeId;
            ////destination.MaterialType = source.Material.MaterialType;
            destination.Name = source.Name;

            foreach (var slot in source.EquippableSlots)
            {
                destination.EquippableSlots.Add(slot);
            }

            destination.Stats.Add(source.Stats);

            destination.Enchantments.Add(source.Enchantments.Select(x => _enchantmentSaver.Save(x)));

            destination.SocketedItems.Add(source.SocketedItems.Select(x => Save(x)));

            destination.Requirements.Class = source.Requirements.Class;
            destination.Requirements.Level = source.Requirements.Level;
            destination.Requirements.Race = source.Requirements.Race;

            return destination;
        }
        #endregion
    }
}
