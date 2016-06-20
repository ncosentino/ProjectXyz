using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;

namespace ProjectXyz.Application.Core.Enchantments
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