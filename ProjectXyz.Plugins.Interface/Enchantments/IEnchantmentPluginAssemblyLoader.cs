using System;
using System.Reflection;
using ProjectXyz.Plugins.Api.Enchantments;

namespace ProjectXyz.Plugins.Interface.Enchantments
{
    public interface IEnchantmentPluginAssemblyLoader
    {
        IEnchantmentPlugin LoadFromAssembly(Assembly assembly, Type type);
    }
}