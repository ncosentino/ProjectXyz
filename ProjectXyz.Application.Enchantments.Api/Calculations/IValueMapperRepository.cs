using System.Collections.Generic;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IValueMapperRepository
    {
        IEnumerable<ValueMapperDelegate> GetValueMappers();
    }
}
