using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Framework.Interface;
using ClassLibrary1.Framework.Shared;

namespace ClassLibrary1.Application.Core.Enchantments
{
    public sealed class EnchantmentContext : IEnchantmentContext
    {
        #region Constructors
        public EnchantmentContext()
        {
            ActiveWeatherDefinitionId = new IntIdentifier(-1);
        }
        #endregion

        #region Properties
        public IIdentifier ActiveWeatherDefinitionId { get; }
        #endregion
    }
}