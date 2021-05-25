
using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Tests.Functional.TestingData
{
    public sealed class WeatherModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<WeatherIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherModifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class WeatherIdentifiers : IWeatherIdentifiers
    {
        public IIdentifier FilterContextWeatherIdentifier { get; } = new StringIdentifier("weather");

        public IIdentifier WeatherIdentifier { get; } = new StringIdentifier("id");
    }

    public sealed class WeatherModifiers : IWeatherModifiers
    {
        public double GetMaximumDuration(IIdentifier weatherId, double baseMaximumDuration)
        {
            return baseMaximumDuration;
        }

        public double GetMinimumDuration(IIdentifier weatherId, double baseMinimumDuration, double maximumDuration)
        {
            return baseMinimumDuration;
        }

        public IReadOnlyDictionary<IIdentifier, double> GetWeights(IReadOnlyDictionary<IIdentifier, double> weatherWeights)
        {
            return weatherWeights;
        }
    }
}
