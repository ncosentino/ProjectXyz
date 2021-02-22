using System.Linq;

using Autofac;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.TimeOfDay;
using ProjectXyz.Plugins.Features.Weather;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Generation
{
    public sealed class FilterContextProviderFunctionalTests
    {
        private static readonly IFilterContextProvider _filterContextProvider;
        private static readonly IWeatherManager _weatherManager;
        private static readonly ITimeOfDayManager _timeOfDayManager;

        static FilterContextProviderFunctionalTests()
        {
            _filterContextProvider = CachedDependencyLoader.LifeTimeScope.Resolve<IFilterContextProvider>();
            _weatherManager = CachedDependencyLoader.LifeTimeScope.Resolve<IWeatherManager>();
            _timeOfDayManager = CachedDependencyLoader.LifeTimeScope.Resolve<ITimeOfDayManager>();            
        }

        [Fact]
        public static void GetFilterContext_Weather_HasSameValueAsManager()
        {
            var expectedValue = _weatherManager.WeatherId;
            var attributes = _filterContextProvider
                .GetContext()
                .Attributes
                .ToArray();
            var attribute = attributes.SingleOrDefault(x => x.Id.Equals(new StringIdentifier("weather")));
            Assert.NotNull(attribute);
            Assert.Equal(expectedValue, ((IdentifierFilterAttributeValue)attribute.Value).Value);
        }

        [Fact]
        public static void GetFilterContext_TimeOfDay_HasSameValueAsManager()
        {
            var expectedValue = _timeOfDayManager.TimeOfDay;
            var attributes = _filterContextProvider
                .GetContext()
                .Attributes
                .ToArray();
            var attribute = attributes.SingleOrDefault(x => x.Id.Equals(new StringIdentifier("time-of-day")));
            Assert.NotNull(attribute);
            Assert.Equal(expectedValue, ((IdentifierFilterAttributeValue)attribute.Value).Value);
        }

        [Fact]
        public static void GetFilterContext_Actors_AttributePresent()
        {
            var attributes = _filterContextProvider
                .GetContext()
                .Attributes
                .ToArray();
            var attribute = attributes.SingleOrDefault(x => x.Id.Equals(new StringIdentifier("actor-stats")));
            Assert.NotNull(attribute);
        }
    }
}
