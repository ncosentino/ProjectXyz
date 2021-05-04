using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class SkillTargetPatternGeneratorComponent : IGeneratorComponent
    {
        public SkillTargetPatternGeneratorComponent(
            IEnumerable<Tuple<int, int>> locations)
        {
            LocationsOffsetFromOrigin = locations.ToArray();
        }

        public IReadOnlyCollection<Tuple<int, int>> LocationsOffsetFromOrigin { get; }
    }

    public sealed class SkillTargetPatternConverter : IBehaviorConverter
    {
        public bool CanConvert(
            IGeneratorComponent component)
        {
            return component is SkillTargetPatternGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var targetPatternComponent = (SkillTargetPatternGeneratorComponent)component;

            return new TargetPatternBehavior(
                targetPatternComponent.LocationsOffsetFromOrigin);
        }
    }
}
