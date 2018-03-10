using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Framework
{
    public sealed class StringIdentifier : IIdentifier
    {
        private readonly IIdentifier _identifier;

        public StringIdentifier(Identifier<string> identifier)
        {
            _identifier = identifier;
        }

        public StringIdentifier(string identifier)
            : this(new Identifier<string>(identifier))
        {
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
            return _identifier.GetHashCode();
        }
    }
}