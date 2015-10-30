﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Core.Stats;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Core.Items.Sockets;
using ProjectXyz.Data.Sql;
using ProjectXyz.Data.Sql.Items.Sockets;
using ProjectXyz.Plugins.Items.Normal.Tests.Integration.Helpers;
using ProjectXyz.Tests.Integration;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Items.Normal.Tests.Integration
{
    [Items]
    [ApplicationLayer]
    public class PluginTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void GenerateItemCallbackInvoke_SimpleDefinition_MatchesDefinition()
        {
            // Setup
            const int LEVEL = 0;

            var dataManager = SqlDataManager.Create(
                Database,
                SqlDatabaseUpgrader.Create());

            var addResult = ItemDefinitionRepositoryHelper.AddItemDefinition(dataManager);

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentCalculatorResultFactory = EnchantmentCalculatorResultFactory.Create();

            var enchantmentCalculator = EnchantmentCalculator.Create(
                enchantmentContext.Object,
                enchantmentCalculatorResultFactory,
                Enumerable.Empty<IEnchantmentTypeCalculator>());

            var enchantmentFactory = EnchantmentFactory.Create();

            var statSocketTypeFactory = StatSocketTypeFactory.Create();
            var statSocketTypeRepository = StatSocketTypeRepository.Create(
                Database,
                statSocketTypeFactory);

            var itemContext = ItemContext.Create(
                enchantmentCalculator,
                enchantmentFactory,
                statSocketTypeRepository);

            var randomizer = new Mock<IRandom>(MockBehavior.Strict);

            var enchantmentApplicationFactoryManager = EnchantmentApplicationFactoryManager.Create();

            var enchantmentApplicationManager = EnchantmentApplicationManager.Create(
                dataManager,
                enchantmentApplicationFactoryManager,
                Enumerable.Empty<IEnchantmentTypeCalculator>());

            var statApplicationManager = StatApplicationManager.Create(dataManager);

            var itemApplicationManager = ItemApplicationManager.Create(
                dataManager,
                enchantmentApplicationManager);

            var plugin = new Plugin(
                Database,
                dataManager,
                statApplicationManager,
                itemApplicationManager);

            // Execute
            var result = plugin.ItemTypeGenerator.Generate(
                randomizer.Object,
                addResult.ItemDefinition.Id,
                LEVEL,
                itemContext);

            // Assert
            Assert.Equal(addResult.ItemDefinition.Id, result.ItemDefinitionId);
            Assert.Equal(addResult.ItemDefinition.InventoryGraphicResourceId, result.InventoryGraphicResourceId);
            Assert.Equal(addResult.ItemDefinition.ItemTypeId, result.ItemTypeId);
            Assert.Equal(addResult.ItemDefinition.MagicTypeId, result.MagicTypeId);
            Assert.Equal(addResult.ItemDefinition.MaterialTypeId, result.MaterialTypeId);
            Assert.Equal(1, result.ItemNameParts.Count());
            Assert.Equal(addResult.ItemDefinition.NameStringResourceId, result.ItemNameParts.First().NameStringResourceId);
            Assert.Equal(addResult.ItemDefinition.SocketTypeId, result.SocketTypeId);
            Assert.Empty(result.Enchantments);
        }
        #endregion
    }
}
