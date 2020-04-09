using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherManager : IReadOnlyWeatherManager
    {
        new IIdentifier WeatherId { get; set; }
    }
}