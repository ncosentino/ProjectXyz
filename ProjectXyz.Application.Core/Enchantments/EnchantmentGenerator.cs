using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentGenerator : IEnchantmentGenerator
    {
        #region Fields
        private readonly IEnchantmentDefinitionRepository _enchantmentDefinitionRepository;
        private readonly IEnchantmentFactory _enchantmentFactory;
        #endregion

        #region Constructors
        private EnchantmentGenerator(
            IEnchantmentDefinitionRepository enchantmentDefinitionRepository,
            IEnchantmentFactory enchantmentFactory)
        {
            Contract.Requires<ArgumentNullException>(enchantmentDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentFactory != null);

            _enchantmentDefinitionRepository = enchantmentDefinitionRepository;
            _enchantmentFactory = enchantmentFactory;
        }
        #endregion

        #region Methods
        public static IEnchantmentGenerator Create(
            IEnchantmentDefinitionRepository enchantmentDefinitionRepository,
            IEnchantmentFactory enchantmentFactory)
        {
            Contract.Requires<ArgumentNullException>(enchantmentDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentFactory != null);
            Contract.Ensures(Contract.Result<IEnchantmentGenerator>() != null);

            return new EnchantmentGenerator(
                enchantmentDefinitionRepository,
                enchantmentFactory);
        }

        public IEnchantment Generate(IRandom randomizer, Guid enchantmentId)
        {
            var definition = _enchantmentDefinitionRepository.GetById(enchantmentId);

            var value = 
                definition.MinimumValue + 
                randomizer.NextDouble() * (definition.MaximumValue - definition.MinimumValue);
            var duration = TimeSpan.FromMilliseconds(
                definition.MinimumDuration.TotalMilliseconds +
                randomizer.NextDouble() * (definition.MaximumDuration.TotalMilliseconds - definition.MinimumDuration.TotalMilliseconds));

            return _enchantmentFactory.Create(
                definition.StatId,
                definition.StatusTypeId,
                definition.TriggerId,
                definition.CalculationId,
                value,
                duration);
        }
        #endregion
    }
}
