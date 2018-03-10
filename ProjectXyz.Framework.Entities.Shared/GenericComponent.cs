using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Shared.Framework.Entities
{
    public sealed class GenericComponent<T> : IComponent<T>
    {
        public GenericComponent(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }

    public static class GenericComponent
    {
        public static IComponent<T> Create<T>(T wrapped)
        {
            return new GenericComponent<T>(wrapped);
        }
    }
}
