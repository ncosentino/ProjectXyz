using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Plugins.Items.Magic
{
    public sealed class MagicItemGenerator : IItemTypeGenerator
    {
        #region Fields
        private readonly Guid _magicType;
        private readonly IItemFactory _itemFactory;
        private readonly IItemMetaDataFactory _itemMetaDataFactory;
        private readonly IItemTypeGenerator _normalItemGenerator;
        private readonly IItemAffixGenerator _itemAffixGenerator;
        private readonly IItemDataManager _itemDataManager;
        #endregion

        #region COnstructors
        private MagicItemGenerator(
            Guid magicType,
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemTypeGenerator normalItemGenerator,
            IItemAffixGenerator itemAffixGenerator,
            IItemDataManager itemDataManager)
        {
            _magicType = magicType;
            _itemFactory = itemFactory;
            _itemMetaDataFactory = itemMetaDataFactory;
            _normalItemGenerator = normalItemGenerator;
            _itemAffixGenerator = itemAffixGenerator;
            _itemDataManager = itemDataManager;
        }
        #endregion

        #region Methods
        public static IItemTypeGenerator Create(
            Guid magicType,
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemTypeGenerator normalItemGenerator,
            IItemAffixGenerator itemAffixGenerator,
            IItemDataManager itemDataManager)
        {
            var generator = new MagicItemGenerator(
                magicType,
                itemFactory,
                itemMetaDataFactory,
                normalItemGenerator,
                itemAffixGenerator,
                itemDataManager);
            return generator;
        }

        public IItem Generate(
            IRandom randomizer,
            Guid itemDefinitionId,
            int level,
            IItemContext itemContext)
        {
            var normalItem = _normalItemGenerator.Generate(
                randomizer,
                itemDefinitionId,
                level,
                itemContext);

            var itemMetaData = _itemMetaDataFactory.Create(
                normalItem.NameStringResourceId,
                normalItem.InventoryGraphicResourceId,
                _magicType,
                normalItem.ItemTypeId,
                normalItem.MaterialTypeId,
                normalItem.SocketTypeId);

            var affixes = GenerateAffixes(
                _itemAffixGenerator,
                randomizer,
                level,
                _magicType)
                .ToArray();

            var enchantments = affixes
                .SelectMany(x => x.Enchantments)
                .Concat(normalItem.Enchantments);

            // TODO: how do we name the item using the affixes now?
            
            var item = _itemFactory.Create(
                itemContext,
                Guid.NewGuid(),
                itemDefinitionId,
                itemMetaData,
                normalItem.Requirements,
                normalItem.Stats,
                enchantments,
                affixes,
                Enumerable.Empty<IItem>(),
                normalItem.EquippableSlotIds);
            return item;
        }

        private IEnumerable<IItemAffix> GenerateAffixes(
            IItemAffixGenerator itemAffixGenerator,
            IRandom randomizer,
            int level, 
            Guid magicTypeId)
        {
            var magicTypesRandomAffixes = _itemDataManager.MagicTypesRandomAffixes.GetForMagicTypeId(magicTypeId);

            var targetAffixCount = 
                (int)Math.Round(magicTypesRandomAffixes.MinimumAffixes + 
                randomizer.NextDouble() * (magicTypesRandomAffixes.MaximumAffixes - magicTypesRandomAffixes.MinimumAffixes));

            bool gotPrefix = false;
            var generatesAffixDefinitionIds = new HashSet<Guid>();

            while (generatesAffixDefinitionIds.Count < targetAffixCount)
            {
                var shouldGeneratePrefix = (magicTypesRandomAffixes.MaximumAffixes != 2 || !gotPrefix) && randomizer.NextDouble() >= 0.5d;

                IItemAffix affix;
                if (shouldGeneratePrefix)
                {
                    affix = itemAffixGenerator.GeneratePrefix(
                        randomizer,
                        level,
                        magicTypeId);
                    gotPrefix = true;
                }
                else
                {
                    affix = itemAffixGenerator.GenerateSuffix(
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
