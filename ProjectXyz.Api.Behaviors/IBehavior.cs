namespace ProjectXyz.Api.Behaviors
{
    public interface IBehavior
    {
        IHasBehaviors Owner { get; }

        void RegisterTo(IHasBehaviors owner);
    }
}