using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments.Api
{
    public interface IValueMapperRepository
    {
        IEnumerable<ValueMapperDelegate> GetValueMappers();
    }
}
