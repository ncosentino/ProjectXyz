using System.Diagnostics;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Framework
{
    [DebuggerDisplay("Identifier<{typeof(T).FullName}>: {_identifier}")]
    public sealed class Identifier<T> : IIdentifier
    {
        private readonly T _identifier;

        public Identifier(T identifier)
        {
            _identifier = identifier;
        }

        public override string ToString()
        {
            return _identifier.ToString();
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