using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class InMemoryTransformativeSocketPatternRepository : IDiscoverableTransformativeSocketPatternRepository
    {
        private readonly IReadOnlyCollection<ITransformativeSocketPatternDefinition> _definitions;

        public InMemoryTransformativeSocketPatternRepository(IEnumerable<ITransformativeSocketPatternDefinition> definitions)
        {
            _definitions = definitions.ToArray();
        }

        public IEnumerable<ITransformativeSocketPatternDefinition> GetAll() => _definitions;
    }
}
