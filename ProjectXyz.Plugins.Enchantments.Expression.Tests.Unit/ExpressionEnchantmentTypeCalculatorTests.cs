using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Weather;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Expression.Tests.Unit
{
    [ApplicationLayer]
    [Enchantments]
    public class ExpressionEnchantmentTypeCalculatorTests
    {
        #region Methods
        [Fact]
        public void Calculate_NoEnchantments_NoDifference()
        {
            // Setup
            var stats = new Mock<IStatCollection>(MockBehavior.Strict);

            var enchantments = new Mock<IMutableEnchantmentCollection>(MockBehavior.Strict);
            enchantments
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IEnchantment>().GetEnumerator());

            var statFactory = new Mock<IStatFactory>(MockBehavior.Strict);

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var expressionEvaluator = new Mock<IExpressionEvaluator>(MockBehavior.Strict);

            var weatherTypeGroupingRepository = new Mock<IWeatherGroupingRepository>(MockBehavior.Strict);

            var enchantmentTypeCalculatorResult = new Mock<IEnchantmentTypeCalculatorResult>(MockBehavior.Strict);

            var enchantmentTypeCalculatorResultFactory = new Mock<IEnchantmentTypeCalculatorResultFactory>(MockBehavior.Strict);
            enchantmentTypeCalculatorResultFactory
                .Setup(x => x.Create(
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.Is<IStatCollection>(s => !s.Any())))
                .Returns(enchantmentTypeCalculatorResult.Object);

            // FIXME: should be mocked... but i'm lazy
            var statCollection = StatCollection.Create();

            var statCollectionFactory = new Mock<IStatCollectionFactory>(MockBehavior.Strict);
            statCollectionFactory
                .Setup(x => x.Create(stats.Object))
                .Returns(statCollection);

            var enchantmentTypeCalculator = ExpressionEnchantmentTypeCalculator.Create(
                statFactory.Object,
                expressionEvaluator.Object,
                weatherTypeGroupingRepository.Object,
                enchantmentTypeCalculatorResultFactory.Object,
                statCollectionFactory.Object);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats.Object, enchantments.Object);

            // Assert
            Assert.Equal(enchantmentTypeCalculatorResult.Object, result);
            
            enchantmentTypeCalculatorResultFactory.Verify(
                x => x.Create(
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.IsAny<IStatCollection>()),
                Times.Once);

            statCollectionFactory.Verify(x => x.Create(stats.Object), Times.Once);
        }
        
        [Fact]
        public void Calculate_MultipleEnchantmentNoStat_OneStatManipulated()
        {
            // Setup
            const double VALUE1 = 123;
            const double VALUE2 = 456;
            var statId = Guid.NewGuid();
            var weatherGroupingId = Guid.NewGuid();
            
            var stats = new Mock<IStatCollection>(MockBehavior.Strict);

            var enchantment1 = new Mock<IExpressionEnchantment>(MockBehavior.Strict);
            enchantment1
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantment1
                .Setup(x => x.WeatherGroupingId)
                .Returns(weatherGroupingId);
            enchantment1
                .Setup(x => x.CalculationPriority)
                .Returns(0);

            var enchantment2 = new Mock<IExpressionEnchantment>(MockBehavior.Strict);
            enchantment2
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantment2
                .Setup(x => x.WeatherGroupingId)
                .Returns(weatherGroupingId);
            enchantment2
                .Setup(x => x.CalculationPriority)
                .Returns(0);

            var enchantments = new IEnchantment[] { enchantment1.Object, enchantment2.Object };

            var stat1 = new Mock<IStat>(MockBehavior.Strict);

            var stat2 = new Mock<IStat>(MockBehavior.Strict);

            var statFactory = new Mock<IStatFactory>(MockBehavior.Strict);
            statFactory
                .Setup(x => x.Create(It.IsAny<Guid>(), statId, VALUE1))
                .Returns(stat1.Object);
            statFactory
                .Setup(x => x.Create(It.IsAny<Guid>(), statId, VALUE2))
                .Returns(stat2.Object);

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var expressionEvaluator = new Mock<IExpressionEvaluator>(MockBehavior.Strict);
            expressionEvaluator
                .Setup(x => x.Evaluate(enchantment1.Object, It.IsAny<IStatCollection>()))
                .Returns(VALUE1);
            expressionEvaluator
              .Setup(x => x.Evaluate(enchantment2.Object, It.IsAny<IStatCollection>()))
              .Returns(VALUE2);

            var weatherTypeGroupingRepository = new Mock<IWeatherGroupingRepository>(MockBehavior.Strict);
            weatherTypeGroupingRepository
                .Setup(x => x.GetByGroupingId(weatherGroupingId))
                .Returns(new IWeatherGrouping[0]);

            var enchantmentTypeCalculatorResult = new Mock<IEnchantmentTypeCalculatorResult>(MockBehavior.Strict);

            var enchantmentTypeCalculatorResultFactory = new Mock<IEnchantmentTypeCalculatorResultFactory>(MockBehavior.Strict);
            enchantmentTypeCalculatorResultFactory
                .Setup(x => x.Create(
                    Enumerable.Empty<IEnchantment>(),
                    new[]
                    {
                        enchantment1.Object,
                        enchantment2.Object,
                    },
                    It.Is<IStatCollection>(s => s.Count == 1 && s.Contains(stat2.Object))))
                .Returns(enchantmentTypeCalculatorResult.Object);

            // FIXME: should be mocked... but i'm lazy
            var statCollection = StatCollection.Create();

            var statCollectionFactory = new Mock<IStatCollectionFactory>(MockBehavior.Strict);
            statCollectionFactory
                .Setup(x => x.Create(stats.Object))
                .Returns(statCollection);

            var enchantmentTypeCalculator = ExpressionEnchantmentTypeCalculator.Create(
                statFactory.Object,
                expressionEvaluator.Object,
                weatherTypeGroupingRepository.Object,
                enchantmentTypeCalculatorResultFactory.Object,
                statCollectionFactory.Object);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats.Object, enchantments);

            // Assert
            Assert.Equal(enchantmentTypeCalculatorResult.Object, result);

            enchantment1.Verify(x => x.StatId, Times.AtLeastOnce);
            enchantment1.Verify(x => x.WeatherGroupingId, Times.Once);
            enchantment1.Verify(x => x.CalculationPriority, Times.Once);

            enchantment2.Verify(x => x.StatId, Times.AtLeastOnce);
            enchantment2.Verify(x => x.WeatherGroupingId, Times.Once);
            enchantment2.Verify(x => x.CalculationPriority, Times.Once);

            statFactory.Verify(x => x.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<double>()), Times.Exactly(2));

            expressionEvaluator.Verify(x => x.Evaluate(It.IsAny<IExpressionEnchantment>(), It.IsAny<IStatCollection>()), Times.Exactly(2));

            weatherTypeGroupingRepository.Verify(x => x.GetByGroupingId(It.IsAny<Guid>()), Times.Exactly(2));

            enchantmentTypeCalculatorResultFactory.Verify(
                x => x.Create(
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.IsAny<IStatCollection>()),
                Times.Once);

            statCollectionFactory.Verify(x => x.Create(stats.Object), Times.Once);
        }
        #endregion
    }
}