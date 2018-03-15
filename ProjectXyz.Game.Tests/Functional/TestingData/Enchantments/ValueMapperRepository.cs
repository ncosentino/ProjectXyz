using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.ExpressionEnchantments.Api;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Enchantments
{
    public sealed class ValueMapperRepository : IValueMapperRepository
    {
        private readonly IInterval _unitInterval;

        public ValueMapperRepository(TestData testData)
        {
            _unitInterval = testData.UnitInterval;
        }

        public IEnumerable<ValueMapperDelegate> GetValueMappers()
        {
            yield return context => new KeyValuePair<string, double>("INTERVAL", context.Elapsed.Divide(_unitInterval));
        }
    }
}