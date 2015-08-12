using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentGenerator : IOneShotNegateEnchantmentGenerator
    {
        #region Fields
        private readonly IOneShotNegateEnchantmentFactory _oneShotNegateEnchantmentFactory;
        #endregion

        #region Constructors
        private OneShotNegateEnchantmentGenerator(IOneShotNegateEnchantmentFactory oneShotNegateEnchantmentFactory)
        {
            _oneShotNegateEnchantmentFactory = oneShotNegateEnchantmentFactory;
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentGenerator Create(IOneShotNegateEnchantmentFactory oneShotNegateEnchantmentFactory)
        {
            var generator = new OneShotNegateEnchantmentGenerator(oneShotNegateEnchantmentFactory);
            return generator;
        }

        public IOneShotNegateEnchantment Generate(
            IRandom randomizer, 
            IOneShotNegateEnchantmentDefinition definition)
        {
            var duration = TimeSpan.FromMilliseconds(
                definition.MinimumDuration.TotalMilliseconds +
                randomizer.NextDouble() * (definition.MaximumDuration.TotalMilliseconds - definition.MinimumDuration.TotalMilliseconds));

            return _oneShotNegateEnchantmentFactory.Create(
                Guid.NewGuid(),
                definition.StatusTypeId,
                definition.TriggerId,
                duration,
                definition.StatId);
        }
        #endregion
    }
}
