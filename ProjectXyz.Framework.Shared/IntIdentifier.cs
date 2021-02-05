using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Framework
{
    public sealed class IntIdentifier : IIdentifier
    {
        private readonly IIdentifier _identifier;

        public IntIdentifier(Identifier<int> identifier)
        {
            _identifier = identifier;
        }

        public IntIdentifier(int identifier)
            : this(new Identifier<int>(identifier))
        {
        }

        public int Identifier => ((Identifier<int>)_identifier).Value;

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