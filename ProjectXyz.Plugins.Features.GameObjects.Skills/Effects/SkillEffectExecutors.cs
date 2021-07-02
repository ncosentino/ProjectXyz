namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public static class SkillEffectExecutors
    {
        public static ISkillEffectExecutor Parallel(
            params ISkillEffectDefinition[] skillEffectDefinitions)
        {
            return new SkillEffectExecutor(true, skillEffectDefinitions);
        }

        public static ISkillEffectExecutor Sequence(
            params ISkillEffectDefinition[] skillEffectDefinitions)
        {
            return new SkillEffectExecutor(false, skillEffectDefinitions);
        }

        public static ISkillEffectExecutor Single(
            ISkillEffectDefinition skillDefinition)
        {
            return new SkillEffectExecutor(false, new[] { skillDefinition });
        }
    }
}
