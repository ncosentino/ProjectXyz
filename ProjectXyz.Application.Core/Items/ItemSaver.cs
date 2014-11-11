using System;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items.Materials;

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
            destination.MagicType = source.MagicType;
            ////destination.MaterialType = source.Material.MaterialType;
            destination.Name = source.Name;

            foreach (var slot in source.EquippableSlots)
            {
                destination.EquippableSlots.Add(slot);
            }

            foreach (var stat in source.Stats)
            {
                destination.Stats.Add(stat);
            }

            foreach (var enchantment in source.Enchantments)
            {
                destination.Enchantments.Add(_enchantmentSaver.Save(enchantment));
            }

            foreach (var item in source.SocketedItems)
            {
                destination.SocketedItems.Add(Save(item));
            }

            destination.Requirements.Class = source.Requirements.Class;
            destination.Requirements.Level = source.Requirements.Level;
            destination.Requirements.Race = source.Requirements.Race;

            return destination;
        }
        #endregion
    }
}
