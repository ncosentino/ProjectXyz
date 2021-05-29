using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class TransformativeSocketPatternRepositoryFacade : ITransformativeSocketPatternRepositoryFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableTransformativeSocketPatternRepository>> _lazyRepositories;

        public TransformativeSocketPatternRepositoryFacade(Lazy<IEnumerable<IDiscoverableTransformativeSocketPatternRepository>> repositories)
        {
            _lazyRepositories = new Lazy<IReadOnlyCollection<IDiscoverableTransformativeSocketPatternRepository>>(() =>
                repositories.Value.ToArray());
        }

        public IEnumerable<ITransformativeSocketPatternDefinition> GetAll() => _lazyRepositories
            .Value
            .SelectMany(x => x.GetAll());
    }
}
