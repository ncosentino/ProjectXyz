using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.TimeOfDay;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class TimeOfDayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<TimeOfDayConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TimeOfDayConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class TimeOfDayConverter : ITimeOfDayConverter
    {
        public IIdentifier GetTimeOfDay(double timeCyclePercent)
        {
            return new StringIdentifier("day");
        }
    }

    public sealed class TimeOfDayConfiguration : ITimeOfDayConfiguration
    {
        public double LengthOfDayInTurns => 10;
    }
}
