﻿using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class SequentialSkillExecutorGeneratorComponent : ISkillExecutorGeneratorComponent
    {
        public SequentialSkillExecutorGeneratorComponent(
            IEnumerable<IIdentifier> skillIdentifiers)
        {
            SkillIdentifiers = skillIdentifiers.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> SkillIdentifiers { get; }
    }

    public sealed class SequentialSkillExecutorConverter : IExecutorBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent component)
        {
            return component is SequentialSkillExecutorGeneratorComponent;
        }

        public ISkillExecutorBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var sequentialComponent = (SequentialSkillExecutorGeneratorComponent)component;

            return new SequentialSkillExecutorBehavior(
                sequentialComponent.SkillIdentifiers);
        }
    }
}