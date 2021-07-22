using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States
{
    public interface IStateIdToTermRepository
    {
        IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings();
    }
}