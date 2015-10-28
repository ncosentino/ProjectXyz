using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.EquipSlots;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Plugins.Items.Normal.Tests.Integration.Helpers
{
    public static class ItemDefinitionRepositoryHelper
    {
        #region Methods
        public static AddItemDefinitionResult AddItemDefinition(IDataManager dataManager)
        {
            var itemDefinitionId = Guid.NewGuid();
            var inventoryGraphicResourceId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();
            var equipSlotTypeId = Guid.NewGuid();

            var displayLanguage = dataManager.Resources.DisplayLanguages.Add(
                Guid.NewGuid(),
                "Dummy Language");
            var nameStringResource = StringResourceRepositoryHelper.AddStringResource(
                dataManager,
                displayLanguage.Id,
                "Item Name String");
            dataManager.Resources.GraphicResources.Add(
                inventoryGraphicResourceId,
                displayLanguage.Id,
                "Dummy Graphic");

            dataManager.Items.SocketTypes.Add(
                socketTypeId,
                nameStringResource.Id);
            dataManager.Items.MaterialTypes.Add(
                materialTypeId,
                nameStringResource.Id);
            dataManager.Items.ItemTypes.Add(
                itemTypeId,
                nameStringResource.Id);
            dataManager.Items.MagicTypes.Add(
                magicTypeId,
                nameStringResource.Id,
                123);
            dataManager.Items.EquipSlotTypes.Add(
                equipSlotTypeId,
                nameStringResource.Id);

            var itemDefinition = dataManager.Items.ItemDefinitions.Add(
                itemDefinitionId,
                nameStringResource.Id,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);

            var itemMiscRequirements = dataManager.Items.ItemMiscRequirements.Add(
                Guid.NewGuid(),
                null,
                null);

            var itemDefinitionItemMiscRequirements = dataManager.Items.ItemDefinitionItemMiscRequirements.Add(
                Guid.NewGuid(),
                itemDefinitionId,
                itemMiscRequirements.Id);

            var itemTypeEquipSlotType = dataManager.Items.ItemTypeEquipSlotType.Add(
                Guid.NewGuid(),
                itemTypeId,
                equipSlotTypeId);

            var addItemDefinitionResult = new AddItemDefinitionResult(
                itemDefinition,
                itemMiscRequirements,
                itemDefinitionItemMiscRequirements,
                itemTypeEquipSlotType);
            return addItemDefinitionResult;
        }
        #endregion
    }

    public class AddItemDefinitionResult
    {
        #region Constructors
        public AddItemDefinitionResult(
            IItemDefinition itemDefinition,
            IItemMiscRequirements itemMiscRequirements,
            IItemDefinitionItemMiscRequirements itemDefinitionItemMiscRequirements,
            IItemTypeEquipSlotType itemTypeEquipSlotType)
        {
            ItemDefinition = itemDefinition;
            ItemMiscRequirements = itemMiscRequirements;
            ItemDefinitionItemMiscRequirements = itemDefinitionItemMiscRequirements;
            ItemTypeEquipSlotType = itemTypeEquipSlotType;
        }
        #endregion

        #region Properties
        public IItemDefinition ItemDefinition { get; private set; }

        public IItemMiscRequirements ItemMiscRequirements { get; private set; }

        public IItemDefinitionItemMiscRequirements ItemDefinitionItemMiscRequirements { get; private set; }

        public IItemTypeEquipSlotType ItemTypeEquipSlotType { get; private set; }
        #endregion
    }
}
