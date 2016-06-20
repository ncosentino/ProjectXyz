using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Plugins.Api.Enchantments;
using ClassLibrary1.Plugins.Interface.Enchantments;

namespace ClassLibrary1.Plugins.Core.Enchantments
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
