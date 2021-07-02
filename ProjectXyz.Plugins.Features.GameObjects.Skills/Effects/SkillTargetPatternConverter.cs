using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
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

    public sealed class SkillTargetPatternConverter : ISkillEffectBehaviorConverter
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
