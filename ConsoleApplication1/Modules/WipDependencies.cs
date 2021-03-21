using System;
using System.Collections.Generic;
using System.Data;

using Autofac;
using ConsoleApplication1.Wip;

using MySql.Data.MySqlClient;

using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Logging;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

namespace ConsoleApplication1.Modules
{
    public sealed class WipDependencies : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatPrinterSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ConsoleLogger>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MySqlConnectionFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherModifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class MySqlConnectionFactory : IConnectionFactory
        {
            public IDbConnection Create()
            {
                var connection = new MySqlConnection(
                    $"Server=localhost;" +
                    $"Database=macerus;" +
                    $"Uid=macerus;" +
                    $"Pwd=macerus;");
                return connection;
            }
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

        public sealed class ActorIdentifiers : IActorIdentifiers
        {
            public IIdentifier FilterContextActorStatsIdentifier { get; } = new StringIdentifier("actor-stats");

            public IIdentifier ActorTypeIdentifier { get; } = new StringIdentifier("actor");

            public IIdentifier AnimationStandBack { get; } = new StringIdentifier("animation-stand-back");

            public IIdentifier AnimationStandForward { get; } = new StringIdentifier("animation-stand-forward");

            public IIdentifier AnimationStandLeft { get; } = new StringIdentifier("animation-stand-left");

            public IIdentifier AnimationStandRight { get; } = new StringIdentifier("animation-stand-right");

            public IIdentifier AnimationWalkBack { get; } = new StringIdentifier("animation-walk-back");

            public IIdentifier AnimationWalkForward { get; } = new StringIdentifier("animation-walk-forward");

            public IIdentifier AnimationWalkLeft { get; } = new StringIdentifier("animation-walk-left");

            public IIdentifier AnimationWalkRight { get; } = new StringIdentifier("animation-walk-right");
        }

        private sealed class ConsoleLogger : ILogger
        {
            public void Debug(string message) =>
                Debug(message, null);

            public void Debug(string message, object data) =>
                Log("DEBUG", message, data);

            public void Error(string message) =>
                Error(message, null);

            public void Error(string message, object data) =>
                Log("ERROR", message, data);

            public void Info(string message) =>
                Info(message, null);

            public void Info(string message, object data) =>
                Log("INFO", message, data);

            public void Warn(string message) =>
                Warn(message, null);

            public void Warn(string message, object data) =>
                Log("WARN", message, data);

            private void Log(string prefix, string message, object data)
            {
                Console.WriteLine($"{prefix}: {message}");
                if (data != null)
                {
                    Console.WriteLine($"\t{data}");
                }
            }
        }
    }
}
