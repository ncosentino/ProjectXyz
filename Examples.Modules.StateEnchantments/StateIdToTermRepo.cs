using System.Collections.Generic;
using ProjectXyz.Plugins.Features.StateEnchantments.Api;

namespace Examples.Modules.StateEnchantments
{
    public sealed class StateIdToTermRepo : IDiscoverableStateIdToTermRepository
    {
        public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
        {
            yield break;
        }
    }
}