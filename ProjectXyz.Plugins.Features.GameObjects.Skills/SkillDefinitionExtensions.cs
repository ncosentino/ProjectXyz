using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public static class SkillDefinitionExtensions
    {
        public static ISkillDefinition WithDisplayIcon(
            this ISkillDefinition oldSkillDefinition,
            string iconResourcePath)
        {
            var displayIconGenerator = new DisplayIconGeneratorComponent(iconResourcePath);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(displayIconGenerator));

            return skillDefinition;
        }

        public static ISkillDefinition WithDisplayName(
            this ISkillDefinition oldSkillDefinition,
            string displayName)
        {
            var displayNameGenerator = new DisplayNameComponentGenerator(displayName);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(displayNameGenerator));

            return skillDefinition;
        }

        public static ISkillDefinition WithSupportedAttributes(
            this ISkillDefinition oldSkillDefinition,
            params IFilterAttribute[] SupportedAttributes)
        {
            var newSupportedAttributes = oldSkillDefinition
                .SupportedAttributes
                .Concat(SupportedAttributes);

            var skillDefinition = new SkillDefinition(
                newSupportedAttributes,
                oldSkillDefinition.FilterComponents);

            return skillDefinition;
        }

        public static ISkillDefinition CanBeUsedInCombat(
            this ISkillDefinition oldSkillDefinition)
        {
            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(new UseInCombatGeneratorComponent()));

            return skillDefinition;
        }

        public static ISkillDefinition WithActorAnimation(
            this ISkillDefinition oldSkillDefinition,
            IIdentifier animationId)
        {
            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(new ActorAnimationOnUseGeneratorComponent(animationId)));

            return skillDefinition;
        }

        public static ISkillDefinition CanBeUsedOutOfCombat(
            this ISkillDefinition oldSkillDefinition)
        {
            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(new UseOutOfCombatGeneratorComponent()));

            return skillDefinition;
        }

        public static ISkillDefinition WithResourceRequirement(
            this ISkillDefinition oldSkillDefinition,
            int resourceId,
            double cost)
        {
            var existingCosts = oldSkillDefinition
                .FilterComponents
                .FirstOrDefault(x => x is StaticResourceRequirementsGeneratorComponent)
                as StaticResourceRequirementsGeneratorComponent;
            if (existingCosts == null)
            {
                var requirementsGenerator = new StaticResourceRequirementsGeneratorComponent(
                    new IntIdentifier(resourceId),
                    cost);

                return new SkillDefinition(
                    oldSkillDefinition.SupportedAttributes,
                    oldSkillDefinition
                        .FilterComponents
                        .AppendSingle(requirementsGenerator));
            }

            var oldSkillDefinitionWithoutCosts = oldSkillDefinition
                .FilterComponents
                .Except(new[] { existingCosts });

            var allCosts = existingCosts
                .StaticResourceRequirements
                .ToDictionary(
                    x => x.Key,
                    x => x.Value);

            allCosts.Add(new IntIdentifier(resourceId), cost);

            var updatedRequirementsGenerator = new StaticResourceRequirementsGeneratorComponent(
                new IntIdentifier(resourceId),
                cost);

            return new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinitionWithoutCosts
                    .AppendSingle(updatedRequirementsGenerator));
        }

        public static ISkillDefinition HasEffects(
            this ISkillDefinition oldSkillDefinition,
            params ISkillEffectExecutor[] skillDefinitionExecutors)
        {
            var executorComponents = skillDefinitionExecutors
                .Select(x => x.IsParallel
                    ? new ParallelEffectExecutorGeneratorComponent(x.EffectDefinitions)
                    : new SequentialEffectExecutorGeneratorComponent(x.EffectDefinitions) as ISkillEffectExecutorGeneratorComponent)
                .ToArray();

            var combinationComponent = new SkillEffectGeneratorComponent(executorComponents);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(combinationComponent));

            return skillDefinition;
        }
    }
}
