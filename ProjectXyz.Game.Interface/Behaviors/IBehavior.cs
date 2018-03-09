namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IBehavior
    {
        IHasBehaviors Owner { get; }

        void RegisterTo(IHasBehaviors owner);
    }
}