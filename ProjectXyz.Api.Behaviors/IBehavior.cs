namespace ProjectXyz.Api.Behaviors
{
    public interface IBehavior
    {
        IHasBehaviors Owner { get; }

        void RegisteringToOwner(IHasBehaviors owner);

        void RegisteredToOwner(IHasBehaviors owner);
    }
}