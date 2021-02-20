using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public interface IHasMutableStatsBehaviorFactory
    {
        IHasMutableStatsBehavior Create();
        
        IHasMutableStatsBehavior Create(IStatManager statManager);
    }
}