﻿using ProjectXyz.Data.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items.EquipSlots;
using ProjectXyz.Data.Interface.Items.MagicTypes;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemDataManager
    {
        #region Properties
        IItemDefinitionRepository ItemDefinitions { get; }
        
        IItemDefinitionStatRepository ItemDefinitionStat { get; }

        IItemTypeEquipSlotTypeRepository ItemTypeEquipSlotType { get; }

        IItemDefinitionStatRequirementsRepository ItemDefinitionStatRequirements { get; }

        IItemMiscRequirementsRepository ItemMiscRequirements { get; }

        IItemDefinitionItemMiscRequirementsRepository ItemDefinitionItemMiscRequirements { get; }

        IItemAffixDefinitionRepository ItemAffixDefinitions { get; }

        IItemAffixDefinitionFilterRepository ItemAffixDefinitionFilter { get; }

        IItemAffixDefinitionEnchantmentRepository ItemAffixDefinitionEnchantment { get; }

        IItemAffixDefinitionMagicTypeGroupingRepository ItemAffixDefinitionMagicTypeGroupings { get; }

        IMagicTypesRandomAffixesRepository MagicTypesRandomAffixes { get; }

        ISocketTypeRepository SocketTypes { get; }

        ISocketPatternDefinitionRepository SocketPatternDefinitions { get; }

        ISocketPatternDefinitionEnchantmentRepository SocketPatternDefinitionEnchantments { get; }

        ISocketPatternDefinitionStatRepository SocketPatternDefinitionStats { get; }

        IMaterialTypeRepository MaterialTypes { get; }

        IItemTypeRepository ItemTypes { get; }

        IItemTypeGroupingRepository ItemTypeGroupings { get; }

        IItemNamePartRepository ItemNameParts { get; }

        IItemNameAffixRepository ItemNameAffixes { get; }

        IItemNameAffixFilter ItemNameAffixFilter { get; }

        IItemNamePartFactory ItemNamePartFactory { get; }

        IItemTypeGeneratorPluginRepository ItemTypeGeneratorPlugins { get; }

        IMagicTypeRepository MagicTypes { get; }

        IMagicTypeGroupingRepository MagicTypeGroupings { get; }

        IEquipSlotTypeRepository EquipSlotTypes { get; }
        #endregion
    }
}
