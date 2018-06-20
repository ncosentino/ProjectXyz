namespace ProjectXyz.Api.Behaviors
{
    public interface IBehaviorManager
    {
        void Register(
            IHasBehaviors owner,
            IBehaviorCollection behaviors);
    }
}