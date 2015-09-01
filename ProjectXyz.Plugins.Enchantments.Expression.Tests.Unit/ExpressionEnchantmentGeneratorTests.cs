using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Weather;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Expression.Tests.Unit
{
    [ApplicationLayer]
    [Enchantments]
    public class ExpressionEnchantmentGeneratorTests
    {
        #region Methods
        [Fact]
        public void Generate_ValidArguments_Success()
        {
            // Setup
            var targetStatId = Guid.NewGuid();
            var sourceStatId = Guid.NewGuid();
            var statusTypeId = Guid.NewGuid();
            var triggerId = Guid.NewGuid();
            var expressionId = Guid.NewGuid();
            var enchantmentWeatherId = Guid.NewGuid();
            var enchantmentDefinitionId = Guid.NewGuid();
            var weatherTypeGroupingId = Guid.NewGuid();
            const string EXPRESSION_STAT_ID = "ExpressionStatId";
            const string EXPRESSION_VALUE_ID = "ExpressionStatId";
            const string EXPRESSION = EXPRESSION_STAT_ID + " " + EXPRESSION_VALUE_ID;
            const double ENCHANTMENT_VALUE = 50;
            const int CALCULATION_PRIORITY = 123;

            var randomizer = new Mock<IRandom>(MockBehavior.Strict);
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0.5)
                .Returns(0.5);

            var enchantment = new Mock<IExpressionEnchantment>(MockBehavior.Strict);

            var expressionDefinition = new Mock<IExpressionDefinition>(MockBehavior.Strict);
            expressionDefinition
                .Setup(x => x.Expression)
                .Returns(EXPRESSION);
            expressionDefinition
                .Setup(x => x.CalculationPriority)
                .Returns(CALCULATION_PRIORITY);

            var expressionDefinitionRepository = new Mock<IExpressionDefinitionRepository>(MockBehavior.Strict);
            expressionDefinitionRepository
                .Setup(x => x.GetById(expressionId))
                .Returns(expressionDefinition.Object);

            var expressionEnchantmentDefinition = new Mock<IExpressionEnchantmentDefinition>(MockBehavior.Strict);
            expressionEnchantmentDefinition
                .Setup(x => x.StatId)
                .Returns(targetStatId);
            expressionEnchantmentDefinition
                .Setup(x => x.ExpressionId)
                .Returns(expressionId);
            expressionEnchantmentDefinition
                .Setup(x => x.MinimumDuration)
                .Returns(TimeSpan.FromSeconds(0));
            expressionEnchantmentDefinition
                .Setup(x => x.MaximumDuration)
                .Returns(TimeSpan.FromSeconds(2));

            var enchantmentDefinition = new Mock<IEnchantmentDefinition>(MockBehavior.Strict);
            enchantmentDefinition
                .Setup(x => x.Id)
                .Returns(enchantmentDefinitionId);
            enchantmentDefinition
                .Setup(x => x.StatusTypeId)
                .Returns(statusTypeId);
            enchantmentDefinition
                .Setup(x => x.TriggerId)
                .Returns(triggerId);

            var expressionStatIds = new[] 
            {
                new KeyValuePair<string, Guid>(EXPRESSION_STAT_ID, sourceStatId) 
            };

            var expressionValues = new[] 
            { 
                new KeyValuePair<string, double>(EXPRESSION_VALUE_ID, ENCHANTMENT_VALUE) 
            };

            var expressionEnchantmentFactory = new Mock<IExpressionEnchantmentFactory>(MockBehavior.Strict);
            expressionEnchantmentFactory
                .Setup(x => x.Create(It.IsAny<Guid>(), statusTypeId, triggerId, weatherTypeGroupingId, TimeSpan.FromSeconds(1), targetStatId, EXPRESSION, CALCULATION_PRIORITY, expressionStatIds, expressionValues))
                .Returns(enchantment.Object);

            var expressionEnchantmentDefinitionRepository = new Mock<IExpressionEnchantmentDefinitionRepository>(MockBehavior.Strict);
            expressionEnchantmentDefinitionRepository
                .Setup(x => x.GetByEnchantmentDefinitionId(enchantmentDefinitionId))
                .Returns(expressionEnchantmentDefinition.Object);

            var expressionEnchantmentValueDefinition = new Mock<IExpressionEnchantmentValueDefinition>(MockBehavior.Strict);
            expressionEnchantmentValueDefinition
                .Setup(x => x.MinimumValue)
                .Returns(0);
            expressionEnchantmentValueDefinition
                .Setup(x => x.MaximumValue)
                .Returns(100);
            expressionEnchantmentValueDefinition
                .Setup(x => x.IdForExpression)
                .Returns(EXPRESSION_VALUE_ID);
            
            var expressionEnchantmentValueDefinitionRepository = new Mock<IExpressionEnchantmentValueDefinitionRepository>(MockBehavior.Strict);
            expressionEnchantmentValueDefinitionRepository
                .Setup(x => x.GetByEnchantmentDefinitionId(enchantmentDefinitionId))
                .Returns(new[] { expressionEnchantmentValueDefinition.Object });

            var expressionEnchantmentStatDefinition = new Mock<IExpressionEnchantmentStatDefinition>(MockBehavior.Strict);
            expressionEnchantmentStatDefinition
                .Setup(x => x.IdForExpression)
                .Returns(EXPRESSION_STAT_ID);
            expressionEnchantmentStatDefinition
                .Setup(x => x.StatId)
                .Returns(sourceStatId);

            var expressionEnchantmentStatDefinitionRepository = new Mock<IExpressionEnchantmentStatDefinitionRepository>(MockBehavior.Strict);
            expressionEnchantmentStatDefinitionRepository
                .Setup(x => x.GetByEnchantmentDefinitionId(enchantmentDefinitionId))
                .Returns(new[] { expressionEnchantmentStatDefinition.Object });

            var enchantmentWeather = new Mock<IEnchantmentDefinitionWeatherGrouping>(MockBehavior.Strict);
            enchantmentWeather
                .Setup(x => x.WeatherTypeGroupingId)
                .Returns(weatherTypeGroupingId);

            var enchantmentDefinitionWeatherGroupingRepository = new Mock<IEnchantmentDefinitionWeatherTypeGroupingRepository>(MockBehavior.Strict);
            enchantmentDefinitionWeatherGroupingRepository
                .Setup(x => x.GetByEnchantmentDefinitionId(enchantmentDefinitionId))
                .Returns(enchantmentWeather.Object);
            
            var enchantmentGenerator = ExpressioneEnchantmentGenerator.Create(
                expressionEnchantmentFactory.Object,
                expressionEnchantmentDefinitionRepository.Object,
                expressionEnchantmentValueDefinitionRepository.Object,
                expressionEnchantmentStatDefinitionRepository.Object,
                expressionDefinitionRepository.Object,
                enchantmentDefinitionWeatherGroupingRepository.Object);

            // Execute
            var result = enchantmentGenerator.Generate(
                randomizer.Object,
                enchantmentDefinition.Object);

            // Assert
            Assert.Equal(enchantment.Object, result);

            randomizer.Verify(x => x.NextDouble(), Times.Exactly(2));

            expressionDefinition.Verify(x => x.Expression, Times.Once);
            expressionDefinition.Verify(x => x.CalculationPriority, Times.Once);

            expressionEnchantmentDefinition.Verify(x => x.StatId, Times.Once);
            expressionEnchantmentDefinition.Verify(x => x.ExpressionId, Times.Once);
            expressionEnchantmentDefinition.Verify(x => x.MinimumDuration, Times.Exactly(2));
            expressionEnchantmentDefinition.Verify(x => x.MaximumDuration, Times.Once);

            enchantmentDefinition.Verify(x => x.Id, Times.Exactly(4));
            enchantmentDefinition.Verify(x => x.StatusTypeId, Times.Once);
            enchantmentDefinition.Verify(x => x.TriggerId, Times.Once);

            expressionEnchantmentFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<IEnumerable<KeyValuePair<string, Guid>>>(),
                    It.IsAny<IEnumerable<KeyValuePair<string, double>>>()),
                Times.Once);

            expressionEnchantmentDefinitionRepository.Verify(x => x.GetByEnchantmentDefinitionId(It.IsAny<Guid>()), Times.Once);

            expressionEnchantmentValueDefinition.Verify(x => x.MinimumValue, Times.Exactly(2));
            expressionEnchantmentValueDefinition.Verify(x => x.MaximumValue, Times.Once);
            expressionEnchantmentValueDefinition.Verify(x => x.IdForExpression, Times.Once);

            expressionEnchantmentValueDefinitionRepository.Verify(x => x.GetByEnchantmentDefinitionId(It.IsAny<Guid>()), Times.Once);

            expressionEnchantmentStatDefinition.Verify(x => x.IdForExpression, Times.Once);
            expressionEnchantmentStatDefinition.Verify(x => x.StatId, Times.Once);

            expressionEnchantmentStatDefinitionRepository.Verify(x => x.GetByEnchantmentDefinitionId(It.IsAny<Guid>()), Times.Once);
            
            enchantmentDefinitionWeatherGroupingRepository.Verify(x => x.GetByEnchantmentDefinitionId(It.IsAny<Guid>()), Times.Once);
            
            enchantmentWeather.Verify(x => x.WeatherTypeGroupingId, Times.Once);
        }
        #endregion
    }
}