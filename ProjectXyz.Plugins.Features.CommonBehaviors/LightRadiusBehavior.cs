using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class LightRadiusBehavior :
        BaseBehavior,
        ILightRadiusBehavior
    {
        public double Radius { get; set; }

        public double Blue { get; set; }

        public double Red { get; set; }

        public double Green { get; set; }

        public double Intensity { get; set; }
    }
}
