using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class ApplySocketEnchantmentsBehaviorFactory : IApplySocketEnchantmentsBehaviorFactory
    {
        public IApplySocketEnchantmentsBehavior Create() =>
            new ApplySocketEnchantmentsBehavior();
    }
}