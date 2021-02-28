
using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Game.Tests.Functional.TestingData
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
        }
    }

    public sealed class WeatherIdentifiers : IWeatherIdentifiers
    {
        public IIdentifier FilterContextWeatherIdentifier { get; } = new StringIdentifier("weather");

        public IIdentifier WeatherIdentifier { get; } = new StringIdentifier("id");
    }
}
