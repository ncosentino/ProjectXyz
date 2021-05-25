using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorStatFilterAttributeValue : IFilterAttributeValue
    {
        public ActorStatFilterAttributeValue(
            IIdentifier actorId,
            IIdentifier statDefinitionId,
            double minimum,
            double maximum)
        {
            StatDefinitionId = statDefinitionId;
            ActorId = actorId;
            Minimum = minimum;
            Maximum = maximum;
        }

        public IIdentifier ActorId { get; }

        public IIdentifier StatDefinitionId { get; }

        public double Minimum { get; }

        public double Maximum { get; }
    }
}