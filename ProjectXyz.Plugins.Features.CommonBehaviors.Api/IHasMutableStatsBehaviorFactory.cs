using ProjectXyz.Api.Enchantments.Stats;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasMutableStatsBehaviorFactory
    {
        IHasMutableStatsBehavior Create();

        IHasMutableStatsBehavior Create(IStatManager statManager);
    }
}