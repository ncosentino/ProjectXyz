namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ILightRadiusBehavior : IReadOnlyLightRadiusBehavior
    {
        new double Intensity { get; set; }

        new double Radius { get; set; }

        new double Blue { get; set; }

        new double Red { get; set; }

        new double Green { get; set; }
    }
}
