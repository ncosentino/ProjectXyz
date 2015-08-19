using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentTypeCalculator : IEnchantmentTypeCalculator
    {
        #region Fields
        private readonly IStatusNegationRepository _statusNegationRepository;
        #endregion

        #region Constructors
        private OneShotNegateEnchantmentTypeCalculator(IStatusNegationRepository statusNegationRepository)
        {
            _statusNegationRepository = statusNegationRepository;
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeCalculator Create(IStatusNegationRepository statusNegationRepository)
        {
            return new OneShotNegateEnchantmentTypeCalculator(statusNegationRepository);
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

            var negationEnchantments = GetActiveOneShotNegateEnchantments(
                enchantmentContext, 
                allEnchantments)
                .ToArray();

            var activeNegations = new Dictionary<Guid, HashSet<IOneShotNegateEnchantment>>();
            foreach (var statusNegation in _statusNegationRepository.GetAll())
            {
                var negatingEnchantments = new HashSet<IOneShotNegateEnchantment>(negationEnchantments.Where(x => x.StatId == statusNegation.StatId));
                if (negationEnchantments.Any())
                {
                    activeNegations[statusNegation.EnchantmentStatusId] = negatingEnchantments;
                }
            }

            foreach (var enchantment in allEnchantments
                .Except(negationEnchantments)
                .Where(x => activeNegations.ContainsKey(x.StatusTypeId)))
            {
                removedEnchantments.Add(enchantment);
                activeNegations.Remove(enchantment.StatusTypeId);
            }

            foreach (var negationEnchantment in negationEnchantments)
            {
                removedEnchantments.Add(negationEnchantment);
            }

            return negationEnchantments;
        }

        private IEnumerable<IOneShotNegateEnchantment> GetActiveOneShotNegateEnchantments(
             IEnchantmentContext enchantmentContext,
             IEnumerable<IEnchantment> enchantments)
        {
            return enchantments
                .Where(x => x is IOneShotNegateEnchantment && (!x.WeatherIds.Any() || x.WeatherIds.Any(e => e == enchantmentContext.ActiveWeatherId)))
                .Cast<IOneShotNegateEnchantment>();
        }
        #endregion
    }
}
