using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Framework.Entities.Shared
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
