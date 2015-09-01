using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentTypeCalculator : IEnchantmentTypeCalculator
    {
        #region Fields
        private readonly IStatusNegationRepository _statusNegationRepository;
        private readonly IWeatherGroupingRepository _weatherGroupingRepository;
        #endregion

        #region Constructors
        private OneShotNegateEnchantmentTypeCalculator(
            IStatusNegationRepository statusNegationRepository,
            IWeatherGroupingRepository weatherGroupingRepository)
        {
            _statusNegationRepository = statusNegationRepository;
            _weatherGroupingRepository = weatherGroupingRepository;
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeCalculator Create(
            IStatusNegationRepository statusNegationRepository,
            IWeatherGroupingRepository weatherGroupingRepository)
        {
            var calculator = new OneShotNegateEnchantmentTypeCalculator(
                statusNegationRepository, 
                weatherGroupingRepository);
            return calculator;
        }

        public IEnchantmentTypeCalculatorResult Calculate(
            IEnchantmentContext enchantmentContext,
            IStatCollection stats, 
            IEnumerable<IEnchantment> enchantments)
        {
            var removedEnchantments = new List<IEnchantment>();
            var processedEnchantments = new List<IEnchantment>(ProcessEnchantments(
                enchantmentContext,
                enchantments,
                removedEnchantments));

            var result = EnchantmentTypeCalculatorResult.Create(
                removedEnchantments,
                processedEnchantments,
                stats);

            return result;
        }

        private IEnumerable<IEnchantment> ProcessEnchantments(
            IEnchantmentContext enchantmentContext,
            IEnumerable<IEnchantment> enchantments,
            IList<IEnchantment> removedEnchantments)
        {
            IEnumerable<IEnchantment> allEnchantments = enchantments as IEnchantment[] ?? enchantments.ToArray();

            var activeNegationEnchantments = GetActiveOneShotNegateEnchantments(
                _weatherGroupingRepository,
                enchantmentContext, 
                allEnchantments)
                .ToArray();

            var activeNegations = new Dictionary<Guid, HashSet<IOneShotNegateEnchantment>>();
            foreach (var statusNegation in _statusNegationRepository.GetAll())
            {
                var negatingEnchantments = new HashSet<IOneShotNegateEnchantment>(activeNegationEnchantments.Where(x => x.StatId == statusNegation.StatId));
                if (activeNegationEnchantments.Any())
                {
                    activeNegations[statusNegation.EnchantmentStatusId] = negatingEnchantments;
                }
            }

            foreach (var enchantment in allEnchantments
                .Except(activeNegationEnchantments)
                .Where(x => activeNegations.ContainsKey(x.StatusTypeId)))
            {
                removedEnchantments.Add(enchantment);
                activeNegations.Remove(enchantment.StatusTypeId);
            }

            foreach (var negationEnchantment in activeNegationEnchantments)
            {
                removedEnchantments.Add(negationEnchantment);
            }

            return activeNegationEnchantments;
        }

        private IEnumerable<IOneShotNegateEnchantment> GetActiveOneShotNegateEnchantments(
            IWeatherGroupingRepository weatherGroupingRepository,
             IEnchantmentContext enchantmentContext,
             IEnumerable<IEnchantment> enchantments)
        {
            return enchantments
                .Where(x =>
                {
                    var weatherGroupings = weatherGroupingRepository
                        .GetByGroupingId(x.WeatherGroupingId)
                        .ToArray();
                    return x is IOneShotNegateEnchantment && (!weatherGroupings.Any() || weatherGroupings.Any(e => e.WeatherId == enchantmentContext.ActiveWeatherId));
                })
                .Cast<IOneShotNegateEnchantment>();
        }
        #endregion
    }
}
