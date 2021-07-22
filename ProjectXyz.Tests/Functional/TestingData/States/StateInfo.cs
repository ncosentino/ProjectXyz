using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Tests.Functional.TestingData.States
{
    public sealed class StateInfo
    {
        public StateTypeIds States { get; } = new StateTypeIds();

        public ElapsedTimeInfo ElapsedTime { get; } = new ElapsedTimeInfo();

        public sealed class StateTypeIds
        {
            public IIdentifier ElapsedTime { get; } = new StringIdentifier("Elapsed Time");
        }

        public sealed class ElapsedTimeInfo
        {
            public IIdentifier ElapsedTimeUnits { get; } = new StringIdentifier("Elapsed Time Units");
        }
    }
}
