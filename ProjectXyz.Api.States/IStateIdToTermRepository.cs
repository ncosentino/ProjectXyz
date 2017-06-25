using System.Collections.Generic;

namespace ProjectXyz.Api.States
{
    public interface IStateIdToTermRepository
    {
        IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings();
    }
}