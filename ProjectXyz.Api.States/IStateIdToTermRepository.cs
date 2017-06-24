using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Api.States
{
    public interface IStateIdToTermRepository : IComponent
    {
        IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings();
    }
}