using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Items.Sockets;
using ProjectXyz.Data.Sql;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Data.Sql.Items.Sockets;
using ProjectXyz.Plugins.Core.Enchantments;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Plugins.Enchantments.Expression.Sql;
using ProjectXyz.Plugins.Items.Normal.Tests.Integration.Helpers;
using ProjectXyz.Tests.Integration;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Items.Magic.Tests.Integration
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
            const int MIN_AFFIXES = 1;
            const int MAX_AFFIXES = 1;

            var dataManager = SqlDataManager.Create(
                Database,
                SqlDatabaseUpgrader.Create());

            var addResult = ItemDefinitionRepositoryHelper.AddItemDefinition(dataManager);

            dataManager.Items.ItemTypeGeneratorPlugins.Add(
                Guid.NewGuid(),
                addResult.ItemDefinition.MagicTypeId,
                typeof(MagicItemGenerator).FullName);

            dataManager.Items.MagicTypesRandomAffixes.Add(
                Guid.NewGuid(),
                addResult.ItemDefinition.MagicTypeId,
                MIN_AFFIXES,
                MAX_AFFIXES);

            var itemAffixDefinition = dataManager.Items.ItemAffixDefinitions.Add(
                Guid.NewGuid(),
                addResult.ItemDefinition.NameStringResourceId,
                false,
                0,
                0);
            
            var enchantmentType = dataManager.Enchantments.EnchantmentTypes.Add(
                Guid.NewGuid(), 
                "Store Repository Class Name",
                typeof(ExpressionEnchantmentDefinitionRepository).AssemblyQualifiedName);

            var enchantmentTrigger = dataManager.Enchantments.EnchantmentTriggers.Add(
                Guid.NewGuid(), 
                addResult.ItemDefinition.NameStringResourceId);

            var enchantmentStatus = dataManager.Enchantments.EnchantmentStatuses.Add(
                Guid.NewGuid(),
                addResult.ItemDefinition.NameStringResourceId);

            var enchantmentDefinition = dataManager.Enchantments.EnchantmentDefinitions.Add(
                Guid.NewGuid(),
                enchantmentType.Id,
                enchantmentTrigger.Id,
                enchantmentStatus.Id);

            var enchantmentStatId = dataManager.Stats.StatDefinitions.Add(
                Guid.NewGuid(),
                addResult.ItemDefinition.NameStringResourceId);

            var weather = dataManager.Weather.Weather.Add(
                Guid.NewGuid(),
                addResult.ItemDefinition.NameStringResourceId);

            var weatherGrouping = dataManager.Weather.WeatherGroupings.Add(
                Guid.NewGuid(),
                weather.Id,
                Guid.NewGuid());

            dataManager.Enchantments.EnchantmentWeather.Add(
                Guid.NewGuid(),
                enchantmentDefinition.Id,
                weatherGrouping.GroupingId);

            var createExpressionEnchantmentArguments = new Dictionary<string, object>()
            {
                { "ExpressionDefinitionId", Guid.NewGuid() },
                { "Expression", "VALUE" },
                { "CalculationPriority", 0 },
                { "ExpressionEnchantmentDefinitionId", Guid.NewGuid() },
                { "EnchantmentDefinitionId", enchantmentDefinition.Id },
                { "StatId", enchantmentStatId.Id },
                { "MinimumDuration", 0 },
                { "MaximumDuration", 0 },
                { "ExpressionEnchantmentValueDefinitionId", Guid.NewGuid() },
                { "IdForExpression", "VALUE" },
                { "MinimumValue", 5 },
                { "MaximumValue", 5 },
            };

            Database.Execute(@"
                INSERT INTO 
                    [ExpressionDefinitions] 
                (
                  [Id],
                  [Expression],
                  [CalculationPriority]
                )
                VALUES
                (
                    @ExpressionDefinitionId,
                    @Expression,
                    @CalculationPriority
                );", createExpressionEnchantmentArguments);

            Database.Execute(@"
                INSERT INTO 
                    [ExpressionEnchantmentDefinitions] 
                (
                  [Id],
                  [EnchantmentDefinitionId],
                  [StatId],
                  [ExpressionId],
                  [MinimumDuration],
                  [MaximumDuration]
                )
                VALUES
                (
                  @ExpressionEnchantmentDefinitionId,
                  @EnchantmentDefinitionId,
                  @StatId,
                  @ExpressionDefinitionId,
                  @MinimumDuration,
                  @MaximumDuration
                );", createExpressionEnchantmentArguments);

            Database.Execute(@"
                INSERT INTO
                    [ExpressionEnchantmentValueDefinitions] 
                (
                  [Id],
                  [EnchantmentDefinitionId],
                  [IdForExpression],
                  [MinimumValue],
                  [MaximumValue]
                )
                VALUES
                (
                    @ExpressionEnchantmentValueDefinitionId,
                    @EnchantmentDefinitionId,
                    @IdForExpression,
                    @MinimumValue,
                    @MaximumValue
                );", createExpressionEnchantmentArguments);

            dataManager.Items.ItemAffixDefinitionEnchantment.Add(
                Guid.NewGuid(),
                itemAffixDefinition.Id,
                enchantmentDefinition.Id);

            var magicTypeGrouping = dataManager.Items.MagicTypeGroupings.Add(
                Guid.NewGuid(),
                Guid.NewGuid(),
                addResult.ItemDefinition.MagicTypeId);

            dataManager.Items.ItemAffixDefinitionMagicTypeGroupings.Add(
                Guid.NewGuid(),
                itemAffixDefinition.Id,
                magicTypeGrouping.GroupingId);

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
            randomizer
                .Setup(x => x.NextDouble())
                .Returns(0);

            var enchantmentApplicationFactoryManager = EnchantmentApplicationFactoryManager.Create();

            var enchantmentApplicationManager = EnchantmentApplicationManager.Create(
                dataManager,
                enchantmentApplicationFactoryManager,
                Enumerable.Empty<IEnchantmentTypeCalculator>());

            var enchantmentPluginRegistrar = EnchantmentPluginRegistrar.Create(
                enchantmentFactory,
                EnchantmentSaver.Create(
                    EnchantmentStoreFactory.Create(),
                    EnchantmentStoreRepository.Create(
                        Database,
                        EnchantmentStoreFactory.Create())),
                enchantmentApplicationManager.EnchantmentGenerator);
            enchantmentPluginRegistrar.RegisterPlugins(new[] { new Enchantments.Expression.Plugin(Database, dataManager, enchantmentApplicationFactoryManager) });

            var applicationManager = ItemApplicationManager.Create(
                dataManager,
                enchantmentApplicationManager);

            var plugin = new Plugin(
                Database,
                dataManager,
                applicationManager);

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
            Assert.Equal(addResult.ItemDefinition.NameStringResourceId, result.NameStringResourceId);
            Assert.Equal(addResult.ItemDefinition.SocketTypeId, result.SocketTypeId);

            Assert.Equal(1, result.Enchantments.Count());
            Assert.IsAssignableFrom<IExpressionEnchantment>(result.Enchantments.First());
            Assert.Equal(enchantmentDefinition.StatusTypeId, result.Enchantments.First().StatusTypeId);
            Assert.Equal(enchantmentDefinition.TriggerId, result.Enchantments.First().TriggerId);
            ////Assert.Equal(enchantmentDefinition.EnchantmentTypeId, result.Enchantments.First().EnchantmentTypeId); // FIXME: this needs to be defined in the database and NOT in code... it's in two spots now.
        }
        #endregion
    }
}
