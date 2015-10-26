using System;
using System.Collections.Generic;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Plugins.Items.Magic
{
    public sealed class MagicAffixGenerator : IMagicAffixGenerator
    {
        #region Fields
        private readonly IMagicTypesRandomAffixesRepository _magicTypesRandomAffixesRepository;
        private readonly IItemAffixGenerator _itemAffixGenerator;
        #endregion

        #region Fields

        private MagicAffixGenerator(
            IMagicTypesRandomAffixesRepository magicTypesRandomAffixesRepository,
            IItemAffixGenerator itemAffixGenerator)
        {
            _magicTypesRandomAffixesRepository = magicTypesRandomAffixesRepository;
            _itemAffixGenerator = itemAffixGenerator;
        }
        #endregion

        #region Methods
        public static IMagicAffixGenerator Create(
            IMagicTypesRandomAffixesRepository magicTypesRandomAffixesRepository,
            IItemAffixGenerator itemAffixGenerator)
        {
            var generator = new MagicAffixGenerator(
                magicTypesRandomAffixesRepository,
                itemAffixGenerator);
            return generator;
        }

        public IEnumerable<IItemAffix> GenerateAffixes(
            IRandom randomizer,
            int level,
            Guid magicTypeId)
        {
            var magicTypesRandomAffixes = _magicTypesRandomAffixesRepository.GetForMagicTypeId(magicTypeId);

            var targetAffixCount =
                (int) Math.Round(magicTypesRandomAffixes.MinimumAffixes +
                randomizer.NextDouble()*
                (magicTypesRandomAffixes.MaximumAffixes - magicTypesRandomAffixes.MinimumAffixes));

            bool gotPrefix = false;
            var generatesAffixDefinitionIds = new HashSet<Guid>();

            while (generatesAffixDefinitionIds.Count < targetAffixCount)
            {
                var shouldGeneratePrefix = 
                    (magicTypesRandomAffixes.MaximumAffixes != 2 || !gotPrefix) &&
                    randomizer.NextDouble() >= 0.5d;

                IItemAffix affix;
                if (shouldGeneratePrefix)
                {
                    affix = _itemAffixGenerator.GeneratePrefix(
                        randomizer,
                        level,
                        magicTypeId);
                    gotPrefix = true;
                }
                else
                {
                    affix = _itemAffixGenerator.GenerateSuffix(
                        randomizer,
                        level,
                        magicTypeId);
                }

                if (generatesAffixDefinitionIds.Contains(affix.ItemAffixDefinitionId))
                {
                    continue;
                }

                generatesAffixDefinitionIds.Add(affix.ItemAffixDefinitionId);
                yield return affix;
            }
        }
        #endregion
    }
}
