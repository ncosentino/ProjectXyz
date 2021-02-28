using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillIdentifiers
    {
        IIdentifier SkillDefinitionIdentifier { get; }

        IIdentifier SkillTypeIdentifier { get; }

        IIdentifier SkillSynergyIdentifier { get; }
    }
}
