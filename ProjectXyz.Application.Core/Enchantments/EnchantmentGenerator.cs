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
        private readonly IEnchantmentDefinitionRepository _enchantmentDefinitionRepository;
        private readonly IEnchantmentPluginRepository _enchantmentPluginRepository;
        private readonly ITypeLoader _typeLoader;
        private readonly Dictionary<Type, GenerateEnchantmentDelegate> _generateEnchantmentMapping;
        #endregion

        #region Constructors
        private EnchantmentGenerator(
            IEnchantmentDefinitionRepository enchantmentDefinitionRepository,
            IEnchantmentPluginRepository enchantmentPluginRepository,
            ITypeLoader typeLoader)
        {
            Contract.Requires<ArgumentNullException>(enchantmentDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentPluginRepository != null);
            Contract.Requires<ArgumentNullException>(typeLoader != null);

            _enchantmentDefinitionRepository = enchantmentDefinitionRepository;
            _enchantmentPluginRepository = enchantmentPluginRepository;
            _typeLoader = typeLoader;
            _generateEnchantmentMapping = new Dictionary<Type, GenerateEnchantmentDelegate>();
        }
        #endregion

        #region Methods
        public static IEnchantmentGenerator Create(
            IEnchantmentDefinitionRepository enchantmentDefinitionRepository,
            IEnchantmentPluginRepository enchantmentPluginRepository,
            ITypeLoader typeLoader)
        {
            Contract.Requires<ArgumentNullException>(enchantmentDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentPluginRepository != null);
            Contract.Requires<ArgumentNullException>(typeLoader != null);
            Contract.Ensures(Contract.Result<IEnchantmentGenerator>() != null);

            return new EnchantmentGenerator(
                enchantmentDefinitionRepository,
                enchantmentPluginRepository, 
                typeLoader);
        }

        /// <inheritdoc />
        public void RegisterCallbackForType<TSpecificType>(GenerateEnchantmentDelegate callbackToRegister)
            where TSpecificType : IEnchantmentDefinition
        {
            RegisterCallbackForType(typeof(TSpecificType), callbackToRegister);
        }

        /// <inheritdoc />
        public void RegisterCallbackForType(Type type, GenerateEnchantmentDelegate callbackToRegister)
        {
            _generateEnchantmentMapping[type] = callbackToRegister;
        }

        /// <inheritdoc />
        public IEnchantment Generate(
            IRandom randomizer,
            Guid enchantmentDefinitionId)
        {
            var enchantmentDefinition = _enchantmentDefinitionRepository.GetById(enchantmentDefinitionId);
            var enchantmentType = _enchantmentPluginRepository.GetByEnchantmentTypeId(enchantmentDefinition.EnchantmentTypeId);
            var definitionRepositoryType = _typeLoader.GetType(enchantmentType.DefinitionRepositoryClassName);
            if (definitionRepositoryType == null)
            {
                throw new InvalidOperationException(string.Format("No enchantment definition repository was found for type '{0}'.", enchantmentType.DefinitionRepositoryClassName));
            }

            var key = _generateEnchantmentMapping.Keys.FirstOrDefault(x => x.IsAssignableFrom(definitionRepositoryType));
            if (key == null)
            {
                throw new InvalidOperationException(string.Format("No callback registered for type '{0}'.", definitionRepositoryType));
            }

            var enchantment = _generateEnchantmentMapping[key].Invoke(
                randomizer,
                enchantmentDefinitionId);
            return enchantment;
        }
        #endregion
    }
}
