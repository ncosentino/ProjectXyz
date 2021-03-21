using System.Collections.Generic;

using ProjectXyz.Plugins.Features.ExpressionEnchantments.Api;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Enchantments
{
    public sealed class ValueMapperRepository : IValueMapperRepository
    {
        public IEnumerable<ValueMapperDelegate> GetValueMappers()
        {
            yield return context => new KeyValuePair<string, double>("INTERVAL", context.ElapsedTurns);
        }
    }
}