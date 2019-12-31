namespace ProjectXyz.Api.Framework.Entities
{
    public interface IComponent
    {

    }

    public interface IComponent<out T> : IComponent
    {
        T Value { get; }
    }
}