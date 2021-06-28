namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IPlayerControlledBehavior : IReadOnlyPlayerControlledBehavior
    {
        new bool IsActive { get; set; }
    }
}
