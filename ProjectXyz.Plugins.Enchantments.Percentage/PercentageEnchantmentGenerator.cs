using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public sealed class PercentageEnchantmentGenerator : IPercentageEnchantmentGenerator
    {
        #region Fields
        private readonly IPercentageEnchantmentFactory _percentageEnchantmentFactory;
        #endregion

        #region Constructors
        private PercentageEnchantmentGenerator(IPercentageEnchantmentFactory percentageEnchantmentFactory)
        {
            _percentageEnchantmentFactory = percentageEnchantmentFactory;
        }
        #endregion

        #region Methods
        public static IPercentageEnchantmentGenerator Create(IPercentageEnchantmentFactory percentageEnchantmentFactory)
        {
            var generator = new PercentageEnchantmentGenerator(percentageEnchantmentFactory);
            return generator;
        }

        public IPercentageEnchantment Generate(
            IRandom randomizer, 
            IPercentageEnchantmentDefinition definition)
        {
            var value = 
                definition.MinimumValue + 
                randomizer.NextDouble() * (definition.MaximumValue - definition.MinimumValue);
            var duration = TimeSpan.FromMilliseconds(
                definition.MinimumDuration.TotalMilliseconds +
                randomizer.NextDouble() * (definition.MaximumDuration.TotalMilliseconds - definition.MinimumDuration.TotalMilliseconds));

            return _percentageEnchantmentFactory.Create(
                Guid.NewGuid(),
                definition.StatusTypeId,
                definition.TriggerId,
                duration,
                definition.StatId,
                value);
        }
        #endregion
    }
}
