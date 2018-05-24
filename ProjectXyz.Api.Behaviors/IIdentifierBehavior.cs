using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Behaviors
{
    public interface IIdentifierBehavior : IReadOnlyIdentifierBehavior
    {
        new IIdentifier Id { get; set; }
    }
}
