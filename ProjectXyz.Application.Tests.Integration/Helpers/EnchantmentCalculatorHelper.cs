using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Weather;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Plugins.Enchantments.OneShotNegate;

namespace ProjectXyz.Application.Tests.Integration.Helpers
{
    public static class EnchantmentCalculatorHelper
    {
        #region Methods
        public static IEnchantmentCalculator CreateEnchantmentCalculator(
            IStatusNegationRepository statusNegationRepository = null,
            IWeatherGroupingRepository weatherGroupingRepository = null,
            IEnchantmentTypeCalculatorResultFactory enchantmentTypeCalculatorResultFactory = null,
            IStatCollectionFactory statCollectionFactory = null)
        {
            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentCalculatorResultFactory = EnchantmentCalculatorResultFactory.Create();

            if (statusNegationRepository == null)
            {
                var curseStatusNegation = StatusNegation.Create(Guid.NewGuid(), ActorStats.Bless, EnchantmentStatuses.Curse);
                var cureStatusNegation = StatusNegation.Create(Guid.NewGuid(), ActorStats.Cure, EnchantmentStatuses.Disease);

                var statusNegations = new[]
                {
                    curseStatusNegation,
                    cureStatusNegation,
                };

                var mockStatusNegationRepository = new Mock<IStatusNegationRepository>(MockBehavior.Strict);
                mockStatusNegationRepository
                    .Setup(x => x.GetAll())
                    .Returns(statusNegations);

                foreach (var statusNegation in statusNegations)
                {
                    mockStatusNegationRepository
                        .Setup(x => x.GetForStatDefinitionId(statusNegation.StatId))
                        .Returns(statusNegation);
                    mockStatusNegationRepository
                        .Setup(x => x.GetForEnchantmentStatusId(statusNegation.EnchantmentStatusId))
                        .Returns(statusNegation);
                }

                statusNegationRepository = mockStatusNegationRepository.Object;
            }

            var statFactory = StatFactory.Create();

            var expressionEvaluator = ExpressionEvaluator.Create(DataTableExpressionEvaluator.Create().Evaluate);

            if (weatherGroupingRepository == null)
            {
                var mockWeatherGroupingRepository = new Mock<IWeatherGroupingRepository>(MockBehavior.Loose);
                mockWeatherGroupingRepository
                    .Setup(x => x.GetByGroupingId(It.IsAny<Guid>()))
                    .Returns(new IWeatherGrouping[0]);
                weatherGroupingRepository = mockWeatherGroupingRepository.Object;
            }

            if (enchantmentTypeCalculatorResultFactory == null)
            {
                enchantmentTypeCalculatorResultFactory = EnchantmentTypeCalculatorResultFactory.Create();
            }

            if (statCollectionFactory == null)
            {
                statCollectionFactory = StatCollectionFactory.Create();
            }

            var enchantmentCalculator = EnchantmentCalculator.Create(
                enchantmentContext.Object,
                enchantmentCalculatorResultFactory,
                new[]
                {
                    OneShotNegateEnchantmentTypeCalculator.Create(
                    statusNegationRepository,
                    weatherGroupingRepository,
                    enchantmentTypeCalculatorResultFactory),
                    ExpressionEnchantmentTypeCalculator.Create(
                        statFactory,
                        expressionEvaluator,
                        weatherGroupingRepository,
                        enchantmentTypeCalculatorResultFactory,
                        statCollectionFactory),
                });

            return enchantmentCalculator;
        }
        #endregion
    }
}
