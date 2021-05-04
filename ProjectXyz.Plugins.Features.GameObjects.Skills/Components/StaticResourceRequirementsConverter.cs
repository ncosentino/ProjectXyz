using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;

using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class StaticResourceRequirementsGeneratorComponent : IGeneratorComponent
    {
        public StaticResourceRequirementsGeneratorComponent(
            IIdentifier identifier,
            double cost) : this(new Dictionary<IIdentifier, double>() { { identifier, cost } })
        {
        }

        public StaticResourceRequirementsGeneratorComponent(
            IReadOnlyDictionary<IIdentifier, double> requirements)
        {
            StaticResourceRequirements = requirements
                .ToDictionary(
                    x => x.Key,
                    x => x.Value);
        }

        public IReadOnlyDictionary<IIdentifier, double> StaticResourceRequirements { get; }
    }

    public sealed class StaticResourceRequirementsConverter : IBehaviorConverter
    {
        public bool CanConvert(
            IGeneratorComponent component)
        {
            return component is StaticResourceRequirementsGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var resourceComponent = (StaticResourceRequirementsGeneratorComponent)component;
            return new SkillResourceUsageBehavior(
                resourceComponent.StaticResourceRequirements);
        }
    }

}
