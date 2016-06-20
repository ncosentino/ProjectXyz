using System;
using System.Reflection;
using ProjectXyz.Plugins.Api.Enchantments;
using ProjectXyz.Plugins.Interface.Enchantments;

namespace ProjectXyz.Plugins.Core.Enchantments
{
    public sealed class EnchantmentPluginAssemblyLoader : IEnchantmentPluginAssemblyLoader
    {
        #region Fields
        private readonly IEnchantmentPluginInitializationProvider _enchantmentPluginInitializationProvider;
        #endregion

        #region Constructors
        public EnchantmentPluginAssemblyLoader(IEnchantmentPluginInitializationProvider enchantmentPluginInitializationProvider)
        {
            _enchantmentPluginInitializationProvider = enchantmentPluginInitializationProvider;
        }
        #endregion

        #region Methods
        public IEnchantmentPlugin LoadFromAssembly(Assembly assembly, Type type)
        {
            var plugin = Activator.CreateInstance(
                type,
                _enchantmentPluginInitializationProvider);
            return (IEnchantmentPlugin)plugin;
        }
        #endregion
    }
}
