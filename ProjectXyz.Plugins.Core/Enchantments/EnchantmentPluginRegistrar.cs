using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Plugins.Enchantments;
using ProjectXyz.Plugins.Interface.Enchantments;

namespace ProjectXyz.Plugins.Core.Enchantments
{
    public sealed class EnchantmentPluginRegistrar : IEnchantmentPluginRegistrar
    {
        #region Fields
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IEnchantmentSaver _enchantmentSaver;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        #endregion

        #region Constructors
        private EnchantmentPluginRegistrar(
            IEnchantmentFactory enchantmentFactory,
            IEnchantmentSaver enchantmentSaver,
            IEnchantmentGenerator enchantmentGenerator)
        {
            Contract.Requires<ArgumentNullException>(enchantmentFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentSaver != null);
            Contract.Requires<ArgumentNullException>(enchantmentGenerator != null);

            _enchantmentFactory = enchantmentFactory;
            _enchantmentSaver = enchantmentSaver;
            _enchantmentGenerator = enchantmentGenerator;
        }
        #endregion

        #region Methods
        public static IEnchantmentPluginRegistrar Create(
            IEnchantmentFactory enchantmentFactory,
            IEnchantmentSaver enchantmentSaver,
            IEnchantmentGenerator enchantmentGenerator)
        {
            Contract.Requires<ArgumentNullException>(enchantmentFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentSaver != null);
            Contract.Requires<ArgumentNullException>(enchantmentGenerator != null);
            Contract.Ensures(Contract.Result<IEnchantmentPluginRegistrar>() != null);

            var registrar = new EnchantmentPluginRegistrar(
                enchantmentFactory,
                enchantmentSaver,
                enchantmentGenerator);
            return registrar;
        }

        public void RegisterPlugins(IEnumerable<IEnchantmentPlugin> enchantmentPlugins)
        {
            foreach (var enchantmentPlugin in enchantmentPlugins)
            {
                _enchantmentSaver.RegisterCallbackForType(
                    enchantmentPlugin.EnchantmentType, 
                    enchantmentPlugin.SaveEnchantmentCallback);    

                _enchantmentGenerator.RegisterCallbackForType(
                    enchantmentPlugin.EnchantmentDefinitionRepositoryType,
                    enchantmentPlugin.GenerateEnchantmentCallback);

                _enchantmentFactory.RegisterCallbackForType(
                    enchantmentPlugin.EnchantmentStoreType,
                    enchantmentPlugin.CreateEnchantmentCallback);
            }
        }
        #endregion
    }
}
