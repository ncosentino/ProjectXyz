using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentTargetBehavior : IBehavior
    {
        IIdentifier Target { get; }
    }
}
