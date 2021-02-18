using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorStatGeneratorAttributeValue : IGeneratorAttributeValue
    {
        public ActorStatGeneratorAttributeValue(
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