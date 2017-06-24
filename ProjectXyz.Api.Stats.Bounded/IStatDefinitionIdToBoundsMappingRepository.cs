using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Api.Stats.Bounded
{
    public interface IStatDefinitionIdToBoundsMappingRepository : IComponent
    {
        IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings();
    }
}
