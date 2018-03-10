using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Game.Tests.Functional.TestingData.States
{
    public sealed class StateInfo
    {
        public StateTypeIds States { get; } = new StateTypeIds();

        public TimeOfDayIds TimeOfDay { get; } = new TimeOfDayIds();

        public ElapsedTimeInfo ElapsedTime { get; } = new ElapsedTimeInfo();

        public sealed class StateTypeIds
        {
            public IIdentifier TimeOfDay { get; } = new StringIdentifier("Time of Day");

            public IIdentifier ElapsedTime { get; } = new StringIdentifier("Elapsed Time");
        }

        public sealed class TimeOfDayIds
        {
            public IIdentifier Day { get; } = new StringIdentifier("Day");

            public IIdentifier Night { get; } = new StringIdentifier("Night");
        }

        public sealed class ElapsedTimeInfo
        {
            public IIdentifier ElapsedTimeUnits { get; } = new StringIdentifier("Elapsed Time Units");
        }
    }
}
