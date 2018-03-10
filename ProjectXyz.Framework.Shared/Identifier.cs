using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Framework
{
    public sealed class Identifier<T> : IIdentifier
    {
        private readonly T _identifier;

        public Identifier(T identifier)
        {
            _identifier = identifier;
        }

        public override string ToString()
        {
            return $"Identifier<{typeof(T).FullName}>: {_identifier}";
        }

        public override bool Equals(object obj)
        {
            return obj != null && (ReferenceEquals(this, obj) || (obj is IIdentifier && Equals((IIdentifier)obj)));
        }

        public bool Equals(IIdentifier other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return _identifier.GetHashCode() ^ 7;
        }
    }

    public static class Identifier
    {
        public static IIdentifier None { get; } = new Identifier<object>(new object());
    }
}