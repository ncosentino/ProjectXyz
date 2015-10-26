using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Affixes;

namespace ProjectXyz.Application.Core.Items.Affixes
{
    public sealed class ItemAffix : IItemAffix
    {
        #region Fields
        private readonly Guid _itemAffixDefinitionId;
        private readonly Guid _nameStringResourceId;
        private readonly bool _prefix;
        private readonly List<IEnchantment> _enchantments;
        #endregion

        #region Constructors
        private ItemAffix(
            Guid itemAffixDefinitionId,
            Guid nameStringResourceId,
            bool prefix,
            IEnumerable<IEnchantment> enchantments)
        {
            _itemAffixDefinitionId = itemAffixDefinitionId;
            _nameStringResourceId = nameStringResourceId;
            _prefix = prefix;
            _enchantments = new List<IEnchantment>(enchantments);
        }
        #endregion

        #region Properties
        public Guid ItemAffixDefinitionId { get { return _itemAffixDefinitionId; } }

        public Guid NameStringResourceId { get { return _nameStringResourceId; } }

        public bool Prefix { get { return _prefix; } }

        public IEnumerable<IEnchantment> Enchantments { get { return _enchantments; } }
        #endregion

        #region Methods
        public static IItemAffix Create(
            Guid itemAffixDefinitionId,
            Guid nameStringResourceId,
            bool prefix,
            IEnumerable<IEnchantment> enchantments)
        {
            var itemAffix = new ItemAffix(
                itemAffixDefinitionId,
                nameStringResourceId,
                prefix,
                enchantments);
            return itemAffix;
        }
        #endregion
    }
}
