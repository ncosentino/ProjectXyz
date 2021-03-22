using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeather : IGameObject
    {
        double DurationInTurns { get; }
        
        IInterval TransitionInDuration { get; }
        
        IInterval TransitionOutDuration { get; }
    }
}