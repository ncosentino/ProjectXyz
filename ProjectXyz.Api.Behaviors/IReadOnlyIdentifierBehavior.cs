using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Behaviors
{
    public interface IReadOnlyIdentifierBehavior : IBehavior
    {
        IIdentifier Id { get; }
    }
}