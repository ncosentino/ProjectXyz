using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.GameObjects
{
    public interface IGameObjectIdentifiers
    {
        IIdentifier FilterContextId { get; }

        IIdentifier FilterContextTemplateId { get; }

        IIdentifier FilterContextTypeId { get; }
    }
}