using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Api.Stats.Bounded
{
    public interface IStatDefinitionIdToBoundsMappingRepository
    {
        IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings();
    }
}
