using System;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public static class EffectDefinitionExtensions
    {
        public static ISkillEffectDefinition Enchant(
            this ISkillEffectDefinition oldSkillDefinition,
            params string[] enchantments)
        {
            var enchantGeneratorComponent = new EnchantTargetsGeneratorComponent(enchantments);

            var skillDefinition = new SkillEffectDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(enchantGeneratorComponent));

            return skillDefinition;
        }

        public static ISkillEffectDefinition EnchantPassive(
            this ISkillEffectDefinition oldSkillDefinition,
            params string[] enchantments)
        {
            var enchantGeneratorComponent = new PassiveEnchantmentGeneratorComponent(enchantments);
            var passiveGeneratorComponent = new PassiveSkillEffectGeneratorComponent();

            var skillDefinition = new SkillEffectDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .Concat(new IGeneratorComponent[] { enchantGeneratorComponent, passiveGeneratorComponent }));

            return skillDefinition;
        }

        public static ISkillEffectDefinition Targets(
            this ISkillEffectDefinition oldSkillDefinition,
            int[] affectedTeams,
            Tuple<int, int> offsetFromUser,
            params Tuple<int, int>[] locations)
        {
            var skillTargetTeamComponent = new SkillTargetCombatTeamGeneratorComponent(affectedTeams);

            var skillOffsetFromUserComponent = new SkillTargetOriginGeneratorComponent(
                offsetFromUser.Item1,
                offsetFromUser.Item2);

            var skillPatternComponent = new SkillTargetPatternGeneratorComponent(locations);

            var skillDefinition = new SkillEffectDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .Concat(new IGeneratorComponent[] { skillTargetTeamComponent, skillOffsetFromUserComponent, skillPatternComponent }));

            return skillDefinition;
        }

        public static ISkillEffectDefinition InflictDamage(
            this ISkillEffectDefinition oldSkillDefinition)
        {
            var skillDefinition = new SkillEffectDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(new InflictDamageGeneratorComponent()));

            return skillDefinition;
        }
    }
}
