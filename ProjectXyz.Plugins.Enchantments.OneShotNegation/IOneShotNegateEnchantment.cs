using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegation
{
    public interface IOneShotNegateEnchantment : IEnchantment
    {
        #region Properties
        IIdentifier StatDefinitionId { get; }
        #endregion
    }
}
