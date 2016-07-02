using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegation
{
    public sealed class EnchantmentTypeCalculator : IEnchantmentTypeCalculator
    {
        #region Fields
        private readonly IReadOnlyCollection<IStatusNegation> _statusNegations;
        private readonly IEnchantmentTypeCalculatorResultFactory _enchantmentTypeCalculatorResultFactory;
       
        #endregion

        #region Constructors
        public EnchantmentTypeCalculator(
            IReadOnlyCollection<IStatusNegation> statusNegations,
            IEnchantmentTypeCalculatorResultFactory enchantmentTypeCalculatorResultFactory)
        {
            _statusNegations = statusNegations;
            _enchantmentTypeCalculatorResultFactory = enchantmentTypeCalculatorResultFactory;
        }
        #endregion

        #region Methods
        public IEnchantmentTypeCalculatorResult Calculate(
            IEnchantmentContext enchantmentContext,
            IStatCollection stats,
            IEnumerable<IEnchantment> enchantments)
        {
            var processResult = ProcessEnchantments(enchantments.ToArray());
            var processedEnchantments = processResult.Item1;
            var removedEnchantments = processResult.Item2;

            var result = _enchantmentTypeCalculatorResultFactory.Create(
                new IEnchantment[0],
                removedEnchantments,
                processedEnchantments,
                stats);
            return result;
        }

        private Tuple<List<IEnchantment>, List<IEnchantment>> ProcessEnchantments(IReadOnlyCollection<IEnchantment> enchantments)
        {
            var processedEnchantments = new List<IEnchantment>();
            var removedEnchantments = new List<IEnchantment>();
            var activeNegationEnchantments = GetActiveOneShotNegateEnchantments(enchantments).ToArray();

            var activeNegations = new Dictionary<IIdentifier, HashSet<IOneShotNegateEnchantment>>();
            foreach (var statusNegation in _statusNegations)
            {
                var negatingEnchantments = new HashSet<IOneShotNegateEnchantment>(activeNegationEnchantments.Where(x => x.StatDefinitionId.Equals(statusNegation.StatDefinitionId)));
                if (activeNegationEnchantments.Any())
                {
                    activeNegations[statusNegation.EnchantmentStatusId] = negatingEnchantments;
                }
            }

            foreach (var enchantment in enchantments
                .Except(activeNegationEnchantments)
                .Where(x => activeNegations.ContainsKey(x.StatusTypeId)))
            {
                removedEnchantments.Add(enchantment);
                activeNegations.Remove(enchantment.StatusTypeId);
            }

            removedEnchantments.AddRange(activeNegationEnchantments);

            return new Tuple<List<IEnchantment>, List<IEnchantment>>(
                processedEnchantments,
                removedEnchantments);
        }

        private IEnumerable<IOneShotNegateEnchantment> GetActiveOneShotNegateEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            return enchantments
                .TakeTypes<IOneShotNegateEnchantment>();
        }
        #endregion
    }
}
