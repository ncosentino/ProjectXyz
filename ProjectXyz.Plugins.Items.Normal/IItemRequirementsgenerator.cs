using System;
using ProjectXyz.Application.Interface.Items.Requirements;

namespace ProjectXyz.Plugins.Items.Normal
{
    public interface IItemRequirementsGenerator
    {
        IItemRequirements GenerateItemRequirements(Guid itemDefinitionId);
    }
}