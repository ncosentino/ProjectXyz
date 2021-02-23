using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations
{
    public sealed class EnchantmentTargetBehavior :
        BaseBehavior,
        IEnchantmentTargetBehavior
    {
        public EnchantmentTargetBehavior(IIdentifier target)
        {
            Target = target;
        }

        public IIdentifier Target { get; }

        public override string ToString()
        {
            return $"'{GetType()}'\r\n\tTarget: {Target}";
        }
    }
}