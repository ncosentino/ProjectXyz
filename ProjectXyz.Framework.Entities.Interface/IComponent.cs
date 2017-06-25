namespace ProjectXyz.Framework.Entities.Interface
{
    public interface IComponent
    {
            
    }

    public interface IComponent<out T> : IComponent
    {
        T Value { get; }
    }
}