using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class RandomItemAffixGenerator : IRandomItemAffixGenerator
    {
        #region Fields
        private readonly IItemAffixGenerator _itemAffixGenerator;
        #endregion

        #region Constructors
        private RandomItemAffixGenerator(IItemAffixGenerator itemAffixGenerator)
        {
            Contract.Requires<ArgumentNullException>(itemAffixGenerator != null);

            _itemAffixGenerator = itemAffixGenerator;
        }
        #endregion

        #region Methods
        public static IRandomItemAffixGenerator Create(IItemAffixGenerator itemAffixGenerator)
        {
            Contract.Requires<ArgumentNullException>(itemAffixGenerator != null);
            Contract.Ensures(Contract.Result<IRandomItemAffixGenerator>() != null);

            return new RandomItemAffixGenerator(itemAffixGenerator);
        }

        public IEnumerable<IEnchantment> GenerateRandomEnchantments(IRandom randomizer, int level, Guid magicTypeId)
        {
            foreach (var affix in _itemAffixGenerator.GenerateRandom(
                randomizer, 
                level, 
                magicTypeId))
            {
                foreach (var enchantment in affix.Enchantments)
                {
                    yield return enchantment;
                }
            }
        }
        #endregion
    }
}
