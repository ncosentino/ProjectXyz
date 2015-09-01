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
        public static AddItemDefinitionResult AddItemDefinition(IDataStore dataStore)
        {
            var itemDefinitionId = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            var inventoryGraphicResourceId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();
            var equipSlotTypeId = Guid.NewGuid();

            var displayLanguage = dataStore.Resources.DisplayLanguages.Add(
                Guid.NewGuid(),
                "Dummy Language");
            dataStore.Resources.StringResources.Add(
                nameStringResourceId, 
                displayLanguage.Id,
                "Dummy String");
            dataStore.Resources.GraphicResources.Add(
                inventoryGraphicResourceId,
                displayLanguage.Id,
                "Dummy Graphic");

            dataStore.Items.SocketTypes.Add(
                socketTypeId,
                nameStringResourceId);
            dataStore.Items.MaterialTypes.Add(
                materialTypeId,
                nameStringResourceId);
            dataStore.Items.ItemTypes.Add(
                itemTypeId,
                nameStringResourceId);
            dataStore.Items.MagicTypes.Add(
                magicTypeId,
                nameStringResourceId);
            dataStore.Items.EquipSlotTypes.Add(
                equipSlotTypeId,
                nameStringResourceId);

            var itemDefinition = dataStore.Items.ItemDefinitions.Add(
                itemDefinitionId,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);

            var itemMiscRequirements = dataStore.Items.ItemMiscRequirements.Add(
                Guid.NewGuid(),
                null,
                null);

            var itemDefinitionItemMiscRequirements = dataStore.Items.ItemDefinitionItemMiscRequirements.Add(
                Guid.NewGuid(),
                itemDefinitionId,
                itemMiscRequirements.Id);

            var itemTypeEquipSlotType = dataStore.Items.ItemTypeEquipSlotType.Add(
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
