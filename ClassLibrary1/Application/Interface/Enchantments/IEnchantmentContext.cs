using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Interface.Enchantments
{
    public interface IEnchantmentContext
    {
        #region Properties
        IIdentifier ActiveWeatherDefinitionId { get; }
        #endregion
    }
}