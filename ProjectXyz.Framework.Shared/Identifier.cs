using System;
using System.Diagnostics;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Framework
{
    [DebuggerDisplay("Identifier<{typeof(T).FullName}>: {_identifier}")]
    public sealed class Identifier<T> : IIdentifier
    {
        private readonly Lazy<string> _lazyToString;

        public Identifier(T value)
        {
            Value = value;
            _lazyToString = new Lazy<string>(Value.ToString);
        }

        public override string ToString() => _lazyToString.Value;

        public T Value { get; }

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
            return Value.GetHashCode() ^ 7;
        }
    }

    public static class Identifier
    {
        public static IIdentifier None { get; } = new Identifier<object>(new object());
    }
}