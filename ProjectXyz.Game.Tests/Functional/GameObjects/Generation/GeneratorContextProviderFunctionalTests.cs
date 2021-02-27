using System.Linq;

using Autofac;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.TimeOfDay;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Generation
{
    public sealed class FilterContextProviderFunctionalTests
    {
        private static readonly IFilterContextProvider _filterContextProvider;
        private static readonly IWeatherManager _weatherManager;
        private static readonly IWeatherFactory _weatherFactory;
        private static readonly ITimeOfDayManager _timeOfDayManager;

        static FilterContextProviderFunctionalTests()
        {
            _filterContextProvider = CachedDependencyLoader.LifeTimeScope.Resolve<IFilterContextProvider>();
            _weatherManager = CachedDependencyLoader.LifeTimeScope.Resolve<IWeatherManager>();
            _weatherFactory = CachedDependencyLoader.LifeTimeScope.Resolve<IWeatherFactory>();
            _timeOfDayManager = CachedDependencyLoader.LifeTimeScope.Resolve<ITimeOfDayManager>();            
        }

        [Fact]
        public static void GetFilterContext_Weather_HasSameValueAsManager()
        {
            var originalWeather = _weatherManager.Weather;
            try
            {
                _weatherManager.Weather = _weatherFactory.Create(
                    new StringIdentifier("the weather"),
                    new Interval<double>(0),
                    new Interval<double>(0),
                    new Interval<double>(0),
                    new IBehavior[] { });
                var expectedValue = _weatherManager
                    .Weather
                    .GetOnly<IIdentifierBehavior>()
                    .Id;
                var attributes = _filterContextProvider
                    .GetContext()
                    .Attributes
                    .ToArray();
                var attribute = attributes.SingleOrDefault(x => x.Id.Equals(new StringIdentifier("weather")));
                Assert.NotNull(attribute);
                Assert.Equal(expectedValue, ((IdentifierFilterAttributeValue)attribute.Value).Value);
            }
            finally
            {
                _weatherManager.Weather = originalWeather;
            }
        }

        [Fact]
        public static void GetFilterContext_WeatherIsNull_NoWeatherAttribute()
        {
            var originalWeather = _weatherManager.Weather;
            try
            {
                _weatherManager.Weather = null;
                var attributes = _filterContextProvider
                    .GetContext()
                    .Attributes
                    .ToArray();
                var attribute = attributes.SingleOrDefault(x => x.Id.Equals(new StringIdentifier("weather")));
                Assert.Null(attribute);
            }
            finally
            {
                _weatherManager.Weather = originalWeather;
            }
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
