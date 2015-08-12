using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public sealed class AdditiveEnchantmentGenerator : IAdditiveEnchantmentGenerator
    {
        #region Fields
        private readonly IAdditiveEnchantmentFactory _additiveEnchantmentFactory;
        #endregion

        #region Constructors
        private AdditiveEnchantmentGenerator(IAdditiveEnchantmentFactory additiveEnchantmentFactory)
        {
            _additiveEnchantmentFactory = additiveEnchantmentFactory;
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentGenerator Create(IAdditiveEnchantmentFactory additiveEnchantmentFactory)
        {
            var generator = new AdditiveEnchantmentGenerator(additiveEnchantmentFactory);
            return generator;
        }

        public IAdditiveEnchantment Generate(
            IRandom randomizer, 
            IAdditiveEnchantmentDefinition definition)
        {
            var value = 
                definition.MinimumValue + 
                randomizer.NextDouble() * (definition.MaximumValue - definition.MinimumValue);
            var duration = TimeSpan.FromMilliseconds(
                definition.MinimumDuration.TotalMilliseconds +
                randomizer.NextDouble() * (definition.MaximumDuration.TotalMilliseconds - definition.MinimumDuration.TotalMilliseconds));

            return _additiveEnchantmentFactory.Create(
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
