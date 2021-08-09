using System.Linq;

namespace ProjectXyz.Plugins.Features.Stats
{
    public static class IStatDefinitionToTermMappingRepositoryExtensions
    {
        public static void WriteStatDefinitionIdToTermMappings(
            IStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            IStatDefinitionToTermMapping statDefinitionToTermMapping)
        {
            statDefinitionToTermMappingRepository.WriteStatDefinitionIdToTermMappings(new[] { statDefinitionToTermMapping });
        }

        public static void WriteStatDefinitionIdToTermMappings(
            IStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            IStatDefinitionToTermMapping statDefinitionToTermMapping,
            params IStatDefinitionToTermMapping[] otherStatDefinitionToTermMapping)
        {
            statDefinitionToTermMappingRepository.WriteStatDefinitionIdToTermMappings(
                new[] { statDefinitionToTermMapping }.Concat(otherStatDefinitionToTermMapping));
        }
    }
}