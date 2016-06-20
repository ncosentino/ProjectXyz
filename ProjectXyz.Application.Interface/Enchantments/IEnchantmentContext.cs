using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentContext
    {
        #region Properties
        IIdentifier ActiveWeatherDefinitionId { get; }
        #endregion
    }
}