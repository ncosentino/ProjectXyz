using ProjectXyz.Api.Enchantments.Stats;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasStatsBehaviorFactory
    {
        IHasStatsBehavior Create();

        IHasStatsBehavior Create(IStatManager statManager);
    }
}