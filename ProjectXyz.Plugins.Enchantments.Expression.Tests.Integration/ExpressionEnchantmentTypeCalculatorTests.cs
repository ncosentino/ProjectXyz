using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Weather;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Expression.Tests.Integration
{
    [ApplicationLayer]
    [Enchantments]
    public class ExpressionEnchantmentTypeCalculatorTests
    {
        #region Methods
        [Fact]
        public void Calculate_AdditiveAndMultiplicative_AdditiveBeforeMultiplicative()
        {
            // Setup
            var statId = Guid.NewGuid();
            var weatherTypeGroupingId = Guid.NewGuid();

            var additiveEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                weatherTypeGroupingId,
                TimeSpan.Zero,
                statId,
                "STAT + VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("STAT", statId) },
                new[] { new KeyValuePair<string, double>("VALUE", 10) } );

            var multiplicativeEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                weatherTypeGroupingId,
                TimeSpan.Zero,
                statId,
                "STAT * VALUE",
                0,
                new[] { new KeyValuePair<string, Guid>("STAT", statId) },
                new[] { new KeyValuePair<string, double>("VALUE", 123) });

            var enchantments = new[]
            {
                additiveEnchantment,
                multiplicativeEnchantment,
            };

            var statFactory = StatFactory.Create();

            var dataTableExpressionEvaluator = DataTableExpressionEvaluator.Create();

            var expressionEvaluator = ExpressionEvaluator.Create(dataTableExpressionEvaluator.Evaluate);

            var weatherTypeGroupingRepository = new Mock<IWeatherGroupingRepository>(MockBehavior.Strict);
            weatherTypeGroupingRepository
                .Setup(x => x.GetByGroupingId(weatherTypeGroupingId))
                .Returns(new IWeatherGrouping[0]);

            var enchantmentTypeCalculatorResultFactory = EnchantmentTypeCalculatorResultFactory.Create();

            var statCollectionFactory = StatCollectionFactory.Create();

            var enchantmentTypeCalculator = ExpressionEnchantmentTypeCalculator.Create(
                statFactory,
                expressionEvaluator,
                weatherTypeGroupingRepository.Object,
                enchantmentTypeCalculatorResultFactory,
                statCollectionFactory);

            var stats = StatCollection.Create();

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(
                enchantmentContext.Object, 
                stats, 
                enchantments);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ProcessedEnchantments.Count());
            Assert.Equal(additiveEnchantment, result.ProcessedEnchantments.First());
            Assert.Equal(multiplicativeEnchantment, result.ProcessedEnchantments.Last());
            Assert.Empty(result.RemovedEnchantments);
            Assert.Equal(1, result.Stats.Count());
            Assert.Equal(statId, result.Stats.First().StatDefinitionId);
            Assert.Equal(1230, result.Stats.First().Value);
        }
        #endregion
    }
}