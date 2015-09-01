using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Items;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Items
{
    [DataLayer]
    [Items]
    public class ItemStoreFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            var inventoryGraphicResourceId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();

            var itemStoreFactory = ItemStoreFactory.Create();

            // Execute
            var result = itemStoreFactory.Create(
                id,
                itemDefinitionId,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(itemDefinitionId, result.ItemDefinitionId);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
            Assert.Equal(inventoryGraphicResourceId, result.InventoryGraphicResourceId);
            Assert.Equal(magicTypeId, result.MagicTypeId);
            Assert.Equal(itemTypeId, result.ItemTypeId);
            Assert.Equal(materialTypeId, result.MaterialTypeId);
            Assert.Equal(socketTypeId, result.SocketTypeId);
        }
    }
}
