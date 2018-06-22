using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IReadOnlyLightRadiusBehavior : IBehavior
    {
        double Intensity { get; }

        double Radius { get; }

        double Blue { get; }

        double Red { get; }

        double Green { get; }
    }
}