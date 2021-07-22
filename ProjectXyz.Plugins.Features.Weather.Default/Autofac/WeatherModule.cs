using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.Weather.Default.Autofac
{
    public sealed class WeatherModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<WeatherMapBehaviorProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherGenerationContextAttributeProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherTableRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherBehaviorsInterceptorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherBehaviorsProviderFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherIdentifiers>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IWeatherIdentifiers))
                .SingleInstance();
            builder
                .RegisterType<WeatherModifiers>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IWeatherModifiers))
                .SingleInstance();
        }
    }
}