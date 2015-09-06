using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items.EquipSlots;
using ProjectXyz.Data.Interface.Items.MagicTypes;
using ProjectXyz.Data.Interface.Items.Materials;
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

        IMagicTypesRandomAffixesRepository MagicTypesRandomAffixes { get; }

        ISocketTypeRepository SocketTypes { get; }

        IMaterialTypeRepository MaterialTypes { get; }

        IItemTypeRepository ItemTypes { get; }

        IMagicTypeRepository MagicTypes { get; }

        IEquipSlotTypeRepository EquipSlotTypes { get; }
        #endregion
    }
}
