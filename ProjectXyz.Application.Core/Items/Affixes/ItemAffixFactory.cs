using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Affixes;

namespace ProjectXyz.Application.Core.Items.Affixes
{
    public sealed class ItemAffixFactory : IItemAffixFactory
    {
        #region Constructors
        private ItemAffixFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemAffixFactory Create()
        {
            var factory = new ItemAffixFactory();
            return factory;
        }

        public IItemAffix Create(
            Guid itemAffixDefinitionId,
            Guid nameStringResourceId,
            IEnumerable<IEnchantment> enchantments)
        {
            var itemAffix = ItemAffix.Create(
                itemAffixDefinitionId,
                nameStringResourceId,
                enchantments);
            return itemAffix;
        }
        #endregion
    }
}
