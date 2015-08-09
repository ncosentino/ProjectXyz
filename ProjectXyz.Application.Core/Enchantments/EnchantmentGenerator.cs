using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentGenerator : IEnchantmentGenerator
    {
        #region Fields
        private readonly IEnchantmentTypeRepository _enchantmentTypeRepository;
        private readonly ITypeLoader _typeLoader;
        private readonly Dictionary<Type, GenerateEnchantmentDelegate> _generateEnchantmentMapping;
        #endregion

        #region Constructors
        private EnchantmentGenerator(
            IEnchantmentTypeRepository enchantmentTypeRepository,
            ITypeLoader typeLoader)
        {
            Contract.Requires<ArgumentNullException>(enchantmentTypeRepository != null);
            Contract.Requires<ArgumentNullException>(typeLoader != null);

            _enchantmentTypeRepository = enchantmentTypeRepository;
            _typeLoader = typeLoader;
            _generateEnchantmentMapping = new Dictionary<Type, GenerateEnchantmentDelegate>();
        }
        #endregion

        #region Methods
        public static IEnchantmentGenerator Create(
            IEnchantmentTypeRepository enchantmentTypeRepository,
            ITypeLoader typeLoader)
        {
            Contract.Requires<ArgumentNullException>(enchantmentTypeRepository != null);
            Contract.Requires<ArgumentNullException>(typeLoader != null);
            Contract.Ensures(Contract.Result<IEnchantmentGenerator>() != null);

            return new EnchantmentGenerator(enchantmentTypeRepository, typeLoader);
        }

        /// <inheritdoc />
        public void RegisterCallbackForType<TSpecificType>(GenerateEnchantmentDelegate callbackToRegister)
            where TSpecificType : IAdditiveEnchantmentDefinition
        {
            RegisterCallbackForType(typeof(TSpecificType), callbackToRegister);
        }

        /// <inheritdoc />
        public void RegisterCallbackForType(Type type, GenerateEnchantmentDelegate callbackToRegister)
        {
            _generateEnchantmentMapping[type] = callbackToRegister;
        }

        /// <inheritdoc />
        public IEnchantment Generate(IRandom randomizer, Guid enchantmentId)
        {
            var definitionRepositoryClassName = _enchantmentTypeRepository.GetDefinitionRepositoryClassName(enchantmentId);
            var definitionRepositoryType = _typeLoader.GetType(definitionRepositoryClassName);
            if (definitionRepositoryType == null)
            {
                throw new InvalidOperationException(string.Format("No enchantment definition repository was found for type '{0}'.", definitionRepositoryClassName));
            }

            if (!_generateEnchantmentMapping.ContainsKey(definitionRepositoryType))
            {
                throw new InvalidOperationException(string.Format("No callback registered for type '{0}'.", definitionRepositoryType));
            }

            var enchantment = _generateEnchantmentMapping[definitionRepositoryType].Invoke(
                randomizer,
                enchantmentId);
            return enchantment;


            ////var value = 
            ////    definition.MinimumValue + 
            ////    randomizer.NextDouble() * (definition.MaximumValue - definition.MinimumValue);
            ////var duration = TimeSpan.FromMilliseconds(
            ////    definition.MinimumDuration.TotalMilliseconds +
            ////    randomizer.NextDouble() * (definition.MaximumDuration.TotalMilliseconds - definition.MinimumDuration.TotalMilliseconds));

            ////return _enchantmentFactory.Create(
            ////    Guid.NewGuid(),
            ////    definition.StatId,
            ////    definition.StatusTypeId,
            ////    definition.TriggerId,
            ////    definition.CalculationId,
            ////    value,
            ////    duration);
        }
        #endregion
    }
}
