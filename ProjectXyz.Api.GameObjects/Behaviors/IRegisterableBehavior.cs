namespace ProjectXyz.Api.GameObjects.Behaviors
{
    public interface IRegisterableBehavior : IBehavior
    {
        void RegisteringToOwner(IGameObject owner);

        void RegisteredToOwner(IGameObject owner);
    }
}