using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ITemplateIdentifierBehavior : IReadOnlyTemplateIdentifierBehavior
    {
        new IIdentifier TemplateId { get; set; }
    }
}
