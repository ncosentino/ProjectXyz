using System;
using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Plugins.Enchantments.OneShotNegation
{
    public interface IOneShotNegateEnchantment : IEnchantment
    {
        #region Properties
        IIdentifier StatDefinitionId { get; }
        #endregion
    }
}
