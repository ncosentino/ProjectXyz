using System.Linq;

using Autofac;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.TimeOfDay;
using ProjectXyz.Plugins.Features.Weather;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Generation
{
    public sealed class GeneratorContextProviderFunctionalTests
    {
        private static readonly IGeneratorContextProvider _generatorContextProvider;
        private static readonly IWeatherManager _weatherManager;
        private static readonly ITimeOfDayManager _timeOfDayManager;

        static GeneratorContextProviderFunctionalTests()
        {
            _generatorContextProvider = CachedDependencyLoader.LifeTimeScope.Resolve<IGeneratorContextProvider>();
            _weatherManager = CachedDependencyLoader.LifeTimeScope.Resolve<IWeatherManager>();
            _timeOfDayManager = CachedDependencyLoader.LifeTimeScope.Resolve<ITimeOfDayManager>();            
        }

        [Fact]
        public static void GetGeneratorContext_Weather_HasSameValueAsManager()
        {
            var expectedValue = _weatherManager.WeatherId;
            var attributes = _generatorContextProvider
                .GetGeneratorContext()
                .Attributes
                .ToArray();
            var attribute = attributes.SingleOrDefault(x => x.Id.Equals(new StringIdentifier("weather")));
            Assert.NotNull(attribute);
            Assert.Equal(expectedValue, ((IdentifierGeneratorAttributeValue)attribute.Value).Value);
        }

        [Fact]
        public static void GetGeneratorContext_TimeOfDay_HasSameValueAsManager()
        {
            var expectedValue = _timeOfDayManager.TimeOfDay;
            var attributes = _generatorContextProvider
                .GetGeneratorContext()
                .Attributes
                .ToArray();
            var attribute = attributes.SingleOrDefault(x => x.Id.Equals(new StringIdentifier("time-of-day")));
            Assert.NotNull(attribute);
            Assert.Equal(expectedValue, ((IdentifierGeneratorAttributeValue)attribute.Value).Value);
        }
    }
}
