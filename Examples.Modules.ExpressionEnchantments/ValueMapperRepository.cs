using System.Collections.Generic;
using ProjectXyz.Plugins.Features.ExpressionEnchantments.Api;

namespace Examples.Modules.ExpressionEnchantments
{
    public sealed class ValueMapperRepository : IValueMapperRepository
    {
        public IEnumerable<ValueMapperDelegate> GetValueMappers()
        {
            yield break;
        }
    }
}