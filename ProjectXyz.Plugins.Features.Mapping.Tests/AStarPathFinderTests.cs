using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Behaviors.Default;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Mapping.Default;
using ProjectXyz.Plugins.Features.Mapping.Default.PathFinding;

using Xunit;

namespace ProjectXyz.Plugins.Features.Mapping.Tests
{
    public sealed class AStarPathFinderTests
    {
        private IPathFinder CreatePathFinder(
            IGameObject map,
            IEnumerable<IGameObject> gameObjects)
        {
            var mapGameObjectManager = new MapGameObjectManager();
            mapGameObjectManager.MarkForAddition(gameObjects);
            mapGameObjectManager.Synchronize();

            var collisionDetector = new BoundingRectangleCollisionDetector(mapGameObjectManager);
            var pathFinder = new AStarPathFinder(map, collisionDetector);
            return pathFinder;
        }

        private IGameObject CreateMap(IEnumerable<IReadOnlyPositionBehavior> tilePositions)
        {
            var map = new GameObject(new[]
            {
                new MapLayersBehavior(new[]
                {
                    new MapLayer(
                        "tiles",
                        tilePositions.Select(pos => new GameObject(new[]
                        {
                            pos
                        }))),
                })
            });
            return map;
        }
        
        private IEnumerable<IReadOnlyPositionBehavior> CreateTileGrid(int width, int height)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    yield return new PositionBehavior(x, y);
                }
            }
        }

        [Fact]
        private void FindPath_SquareGridCornerToCorner_DiagonalPath()
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var pathFinder = CreatePathFinder(map, Enumerable.Empty<IGameObject>());

            var path = pathFinder
                .FindPath(
                    startPosition: new Vector2(0, 0),
                    endPosition: new Vector2(4, 4),
                    size: new Vector2(1, 1))
                .ToArray();

            Assert.Equal(
                new[]
                {
                    new Vector2(1, 1),
                    new Vector2(2, 2),
                    new Vector2(3, 3),
                    new Vector2(4, 4)
                },
                path);
        }

        [Fact]
        private void FindPath_SquareGridCornerToCornerWithSingleCollision_AvoidsCollision()
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var gameObjects = new[]
            {
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 1),
                    new PositionBehavior(1, 1),
                }),
            };
            var pathFinder = CreatePathFinder(map, gameObjects);

            var path = pathFinder
                .FindPath(
                    new Vector2(0, 0),
                    new Vector2(4, 4),
                    new Vector2(1, 1))
                .ToArray();
            Assert.Equal(
                new[]
                {
                    new Vector2(0, 1),
                    new Vector2(0, 2),
                    new Vector2(1, 3),
                    new Vector2(2, 4),
                    new Vector2(3, 4),
                    new Vector2(4, 4)
                },
                path);
        }

        [Fact]
        private void FindPath_SquareGridCornerToCornerCoveredTarget_NoPath()
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var gameObjects = new[]
            {
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 1),
                    new PositionBehavior(4, 4),
                }),
            };
            var pathFinder = CreatePathFinder(map, gameObjects);

            var path = pathFinder
                .FindPath(
                    new Vector2(0, 0),
                    new Vector2(4, 4),
                    new Vector2(1, 1))
                .ToArray();
            Assert.Empty(path);
        }

        // 4 o o o X o
        // 3 o X o X o
        // 2 o X o X o
        // 1 o X o X o 
        // 0 o X o o o
        //   0 1 2 3 4
        [Fact]
        private void FindPath_ZigZagAroundCollisions_ExpectedPath()
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var gameObjects = new[]
            {
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 4),
                    new PositionBehavior(1, 1.5),
                }),
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 4),
                    new PositionBehavior(3, 2.5),
                }),
            };
            var pathFinder = CreatePathFinder(map, gameObjects);

            var path = pathFinder
                .FindPath(
                    new Vector2(0, 0),
                    new Vector2(4, 4),
                    new Vector2(1f, 1f))
                .ToArray();
            Assert.Equal(
                new[]
                {
                    new Vector2(0, 1),
                    new Vector2(0, 2),
                    new Vector2(0, 3),
                    new Vector2(0, 4),
                    new Vector2(1, 4),
                    new Vector2(2, 4),
                    new Vector2(2, 3),
                    new Vector2(2, 2),
                    new Vector2(2, 1),
                    new Vector2(2, 0),
                    new Vector2(3, 0),
                    new Vector2(4, 0),
                    new Vector2(4, 1),
                    new Vector2(4, 2),
                    new Vector2(4, 3),
                    new Vector2(4, 4),
                },
                path);
        }

        // 4 X o o o X
        // 3 o o o o o
        // 2 o o o o o
        // 1 o o o o o 
        // 0 X o o o X
        //   0 1 2 3 4
        [ClassData(typeof(AdjacentToCornersTestData))]
        [Theory]
        private void GetAdjacentPositionsToTile_MapCornersNoColisions_AdjacentTiles(
            Vector2 target,
            IReadOnlyCollection<Vector2> expectedPoints)
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var gameObjects = new IGameObject[] { };
            var pathFinder = CreatePathFinder(map, gameObjects);

            var path = pathFinder
                .GetAdjacentPositionsToTile(target, true)
                .ToArray();
            Assert.Equal(
                expectedPoints.OrderBy(x => x.ToString()),
                path.OrderBy(x => x.ToString()));
        }

        private sealed class AdjacentToCornersTestData : IEnumerable<object[]>
        {
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new Vector2(0, 0),
                    new[] { new Vector2(0,1), new Vector2(1,1), new Vector2(1,0) },
                };
                yield return new object[]
                {
                    new Vector2(0, 4),
                    new[] { new Vector2(0,3), new Vector2(1,3), new Vector2(1,4) },
                };
                yield return new object[]
                {
                    new Vector2(4, 4),
                    new[] { new Vector2(3,4), new Vector2(3,3), new Vector2(4,3) },
                };
                yield return new object[]
                {
                    new Vector2(4, 0),
                    new[] { new Vector2(3,0), new Vector2(3,1), new Vector2(4,1) },
                };
            }
        }
    }
}
