using System;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class NamedItemAffixGenerator : INamedItemAffixGenerator
    {
        #region Fields
        private readonly IItemAffixGenerator _itemAffixGenerator;
        #endregion

        #region Constructors
        private NamedItemAffixGenerator(IItemAffixGenerator itemAffixGenerator)
        {
            Contract.Requires<ArgumentNullException>(itemAffixGenerator != null);

            _itemAffixGenerator = itemAffixGenerator;
        }
        #endregion

        #region Methods
        public static INamedItemAffixGenerator Create(IItemAffixGenerator itemAffixGenerator)
        {
            Contract.Requires<ArgumentNullException>(itemAffixGenerator != null);
            Contract.Ensures(Contract.Result<INamedItemAffixGenerator>() != null);

            return new NamedItemAffixGenerator(itemAffixGenerator);
        }

        public INamedItemAffixes GenerateRandomNamedAffixes(IRandom randomizer, int level, Guid magicTypeId)
        {
            INamedItemAffix prefix = null;
            INamedItemAffix suffix = null;

            var generatePrefix = randomizer.NextDouble() >= 0.5;
            if (generatePrefix)
            {
                prefix = _itemAffixGenerator.GeneratePrefix(randomizer, level, magicTypeId);
            }

            if (!generatePrefix || randomizer.NextDouble() >= 0.5)
            {
                suffix = _itemAffixGenerator.GenerateSuffix(randomizer, level, magicTypeId);
            }

            return NamedItemAffixes.Create(prefix, suffix);
        }
        #endregion
    }
}
