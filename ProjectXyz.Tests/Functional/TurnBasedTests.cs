using System.Linq;

using Autofac;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Behaviors.Default;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.TurnBased;
using ProjectXyz.Tests.Functional.TestingData;

using Xunit;

namespace ProjectXyz.Tests.Functional
{
    public sealed class TurnBasedTests
    {
        private static readonly TestFixture _fixture;

        static TurnBasedTests()
        {
            _fixture = new TestFixture(new TestData());
        }

        [Fact]
        private void XXX()
        {
            var turnBasedManager = _fixture.LifeTimeScope.Resolve<ITurnBasedManager>();
            var mapGameObjectManager = _fixture.LifeTimeScope.Resolve<IMapGameObjectManager>();
            Assert.Empty(mapGameObjectManager.GameObjects);

            try
            {
                var initialGameObjects = new[]
                {
                    new GameObject(Enumerable.Empty<IBehavior>()),
                    new GameObject(Enumerable.Empty<IBehavior>()),
                    new GameObject(Enumerable.Empty<IBehavior>())
                };
                mapGameObjectManager.MarkForAddition(initialGameObjects);
                mapGameObjectManager.Synchronize();

                turnBasedManager.GlobalSync = true;

                using (var enumerator = turnBasedManager.GetApplicableGameObjects().GetEnumerator())
                {
                    Assert.True(enumerator.MoveNext());
                    Assert.Equal(initialGameObjects[0], enumerator.Current);

                    // simulating an asynchronous bit of code chaning map
                    // objects on us... we shouldn't get killed by this
                    // happening mid enumeration!
                    mapGameObjectManager.MarkForRemoval(initialGameObjects);
                    mapGameObjectManager.Synchronize();

                    Assert.True(enumerator.MoveNext());
                    Assert.Equal(initialGameObjects[1], enumerator.Current);
                    Assert.True(enumerator.MoveNext());
                    Assert.Equal(initialGameObjects[2], enumerator.Current);
                    Assert.False(enumerator.MoveNext());
                }
            }
            finally
            {
                mapGameObjectManager.ClearGameObjectsImmediate();
            }
        }
    }
}
