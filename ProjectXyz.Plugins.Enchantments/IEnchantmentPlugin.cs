using System;
using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments
{
    public interface IEnchantmentPlugin
    {
        #region Properties
        Type EnchantmentStoreType { get; }

        Type EnchantmentDefinitionType { get; }

        Type EnchantmentType { get; }

        IEnchantmentTypeCalculator EnchantmentTypeCalculator { get; }

        CreateEnchantmentDelegate CreateEnchantmentCallback { get; }

        SaveEnchantmentDelegate SaveEnchantmentCallback { get; }

        GenerateEnchantmentDelegate GenerateEnchantmentCallback { get; }
        #endregion
    }
}
