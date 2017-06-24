using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IValueMapperRepository : IComponent
    {
        IEnumerable<ValueMapperDelegate> GetValueMappers();
    }
}
