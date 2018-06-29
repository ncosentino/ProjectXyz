using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation
{
    public sealed class GeneratorContextProvider : IGeneratorContextProvider
    {
        private readonly IGeneratorContextFactory _generatorContextFactory;
        private readonly IReadOnlyCollection<IGeneratorContextAttributeProvider> _generatorContextAttributeProviders;

        public GeneratorContextProvider(
            IGeneratorContextFactory generatorContextFactory,
            IEnumerable<IGeneratorContextAttributeProvider> generatorContextAttributeProviders)
        {
            _generatorContextFactory = generatorContextFactory;
            _generatorContextAttributeProviders = generatorContextAttributeProviders.ToArray();
        }

        public IGeneratorContext GetGeneratorContext()
        {
            var attributes = _generatorContextAttributeProviders.SelectMany(x => x.GetAttributes());
            var newContext = _generatorContextFactory.CreateGeneratorContext(
                0,
                int.MaxValue,
                attributes);
            return newContext;
        }
    }
}