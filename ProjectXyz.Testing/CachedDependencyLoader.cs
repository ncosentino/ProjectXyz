using System;

using Autofac;

namespace ProjectXyz.Testing
{
    public static class CachedDependencyLoader
    {
        private static readonly Lazy<ILifetimeScope> _scope = new Lazy<ILifetimeScope>(
            () => Load());

        public static ILifetimeScope LifeTimeScope => _scope.Value;

        private static ILifetimeScope Load()
        {
            var testLifeTimeScopeFactory = new TestLifeTimeScopeFactory();
            var scope = testLifeTimeScopeFactory.CreateScope();
            return scope;
        }
    }
}
