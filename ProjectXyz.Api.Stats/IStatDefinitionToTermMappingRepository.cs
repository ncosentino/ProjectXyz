using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Api.Stats
{
    public interface IStatDefinitionToTermMappingRepository : IComponent
    {
        IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings();
    }
}