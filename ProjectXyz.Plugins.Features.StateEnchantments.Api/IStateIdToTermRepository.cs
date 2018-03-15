using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.StateEnchantments.Api
{
    public interface IStateIdToTermRepository
    {
        IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings();
    }
}