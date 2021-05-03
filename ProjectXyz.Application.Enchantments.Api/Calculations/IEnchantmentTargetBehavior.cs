using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentTargetBehavior : IBehavior
    {
        IIdentifier Target { get; }
    }
}
