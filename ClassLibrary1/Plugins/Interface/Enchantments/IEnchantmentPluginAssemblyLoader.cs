using System;
using System.Reflection;
using ClassLibrary1.Plugins.Api.Enchantments;

namespace ClassLibrary1.Plugins.Interface.Enchantments
{
    public interface IEnchantmentPluginAssemblyLoader
    {
        IEnchantmentPlugin LoadFromAssembly(Assembly assembly, Type type);
    }
}