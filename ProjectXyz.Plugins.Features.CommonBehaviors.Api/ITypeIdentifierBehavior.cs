using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ITypeIdentifierBehavior : IReadOnlyTypeIdentifierBehavior
    {
        new IIdentifier TypeId { get; set; }
    }
}
