using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IIdentifierBehavior : IReadOnlyIdentifierBehavior
    {
        new IIdentifier Id { get; set; }
    }
}
