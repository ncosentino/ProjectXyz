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
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Plugins.Enchantments.OneShotNegate;

namespace ProjectXyz.Application.Tests.Integration.Helpers
{
    public static class EnchantmentCalculatorHelper
    {
        #region Methods
        public static IEnchantmentCalculator CreateEnchantmentCalculator(
            IStatusNegationRepository statusNegationRepository = null)
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
                        .Setup(x => x.GetForStatId(statusNegation.StatId))
                        .Returns(statusNegation);
                    mockStatusNegationRepository
                        .Setup(x => x.GetForEnchantmentStatusId(statusNegation.EnchantmentStatusId))
                        .Returns(statusNegation);
                }

                statusNegationRepository = mockStatusNegationRepository.Object;
            }

            var statFactory = StatFactory.Create();

            var expressionEvaluator = ExpressionEvaluator.Create(DataTableExpressionEvaluator.Create().Evaluate);

            var enchantmentCalculator = EnchantmentCalculator.Create(
                enchantmentContext.Object,
                enchantmentCalculatorResultFactory,
                new[]
                {
                    OneShotNegateEnchantmentTypeCalculator.Create(statusNegationRepository),
                    ExpressionEnchantmentTypeCalculator.Create(
                        statFactory,
                        expressionEvaluator),
                });

            return enchantmentCalculator;
        }
        #endregion
    }
}
