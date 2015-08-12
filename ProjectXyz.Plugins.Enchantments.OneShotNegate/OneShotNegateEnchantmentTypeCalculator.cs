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
        private OneShotNegateEnchantmentTypeCalculator(
            IStatusNegationRepository statusNegationRepository)
        {
            _statusNegationRepository = statusNegationRepository;
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeCalculator Create(IStatusNegationRepository statusNegationRepository)
        {
            return new OneShotNegateEnchantmentTypeCalculator(statusNegationRepository);
        }

        public IEnchantmentTypeCalculatorResult Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments)
        {
            var removedEnchantments = new List<IEnchantment>();
            var processedEnchantments = new List<IEnchantment>(ProcessEnchantments(
                enchantments,
                removedEnchantments));

            var result = EnchantmentTypeCalculatorResult.Create(
                removedEnchantments,
                processedEnchantments,
                stats);

            return result;
        }

        private IEnumerable<IEnchantment> ProcessEnchantments(
            IEnumerable<IEnchantment> enchantments,
            IList<IEnchantment> removedEnchantments)
        {
            var negationEnchantments = enchantments
                .Where(x => x is IOneShotNegateEnchantment)
                .Cast<IOneShotNegateEnchantment>()
                .ToArray();

            var activeNegations = new Dictionary<Guid, bool>();
            foreach (var statusNegation in _statusNegationRepository.GetAll())
            {
                activeNegations[statusNegation.EnchantmentStatusId] = negationEnchantments.Any(x => x.StatId == statusNegation.StatId);
            }

            foreach (var enchantment in enchantments
                .Except(negationEnchantments)
                .Where(x =>
                    activeNegations.ContainsKey(x.StatusTypeId) &&
                    activeNegations[x.StatusTypeId]))
            {
                removedEnchantments.Add(enchantment);
            }

            return negationEnchantments;
        }
        #endregion
    }
}
