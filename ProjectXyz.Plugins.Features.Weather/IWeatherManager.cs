using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherManager : IReadOnlyWeatherManager
    {
        new IIdentifier WeatherId { get; set; }
    }
}