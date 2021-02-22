using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class TemplateIdentifierBehavior :
       BaseBehavior,
       ITemplateIdentifierBehavior
    {
        public TemplateIdentifierBehavior()
            : this(null)
        {
        }

        public TemplateIdentifierBehavior(IIdentifier templateId)
        {
            TemplateId = templateId;
        }

        public IIdentifier TemplateId { get; set; }
    }
}
