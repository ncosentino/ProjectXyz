using System;
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
        private void FindPath_SquareGridCornerToCornerWithDiagonals_DiagonalPath()
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var pathFinder = CreatePathFinder(map, Enumerable.Empty<IGameObject>());

            var path = pathFinder.FindPath(
                startPosition: new Vector2(0, 0),
                endPosition: new Vector2(4, 4),
                size: new Vector2(1, 1),
                includeDiagonals: true);

            Assert.Equal(
                new[]
                {
                    new Vector2(1, 1),
                    new Vector2(2, 2),
                    new Vector2(3, 3),
                    new Vector2(4, 4)
                },
                path.Positions);
            Assert.Equal<double>(
                new double[]
                {
                    1.41421,
                    2.82843,
                    4.24264,
                    5.65685
                },
                path.AccumulatedDistancePerPoint.OrderBy(x => x.Value).Select(x => Math.Round(x.Value, 5)));
            Assert.Equal(
                5.6568542495,
                Math.Round(path.TotalDistance, 10));
        }

        [Fact]
        private void FindPath_SquareGridCornerToCornerWithSingleCollisionWithDiagonals_AvoidsCollision()
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

            var path = pathFinder.FindPath(
                new Vector2(0, 0),
                new Vector2(4, 4),
                new Vector2(1, 1),
                includeDiagonals: true);
            
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
                path.Positions);
            Assert.Equal(
                new double[]
                {
                    1d,
                    2d,
                    3.41421,
                    4.82843,
                    5.82843,
                    6.82843,
                },
                path.AccumulatedDistancePerPoint.OrderBy(x => x.Value).Select(x => Math.Round(x.Value, 5)));
            Assert.Equal(
                6.8284271247,
                Math.Round(path.TotalDistance, 10));
        }

        [Fact]
        private void FindPath_SquareGridCornerToCornerCoveredTargetWithDiagonals_NoPath()
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

            var path = pathFinder.FindPath(
                new Vector2(0, 0),
                new Vector2(4, 4),
                new Vector2(1, 1),
                includeDiagonals: true);
            Assert.Equal(
                Path.Empty,
                path);
        }

        // 4 o o o X o
        // 3 o X o X o
        // 2 o X o X o
        // 1 o X o X o 
        // 0 o X o o o
        //   0 1 2 3 4
        [Fact]
        private void FindPath_ZigZagAroundCollisionsWithDiagonals_ExpectedPath()
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

            var path = pathFinder.FindPath(
                new Vector2(0, 0),
                new Vector2(4, 4),
                new Vector2(1f, 1f),
                includeDiagonals: true);
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
                path.Positions);
            Assert.Equal(
                new double[]
                {
                    1d,
                    2d,
                    3d,
                    4d,
                    5d,
                    6d,
                    7d,
                    8d,
                    9d,
                    10d,
                    11d,
                    12d,
                    13d,
                    14d,
                    15d,
                    16d,
                },
                path.AccumulatedDistancePerPoint.OrderBy(x => x.Value).Select(x => Math.Round(x.Value, 5)));
            Assert.Equal(
                16,
                Math.Round(path.TotalDistance, 10));
        }

        // 4 X o o o X
        // 3 o o o o o
        // 2 o o o o o
        // 1 o o o o o 
        // 0 X o o o X
        //   0 1 2 3 4
        [ClassData(typeof(AdjacentToCornersTestData))]
        [Theory]
        private void GetAllAdjacentPositionsToTile_MapCornersNoColisions_AdjacentTiles(
            Vector2 target,
            IReadOnlyCollection<Vector2> expectedPoints)
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var gameObjects = new IGameObject[] { };
            var pathFinder = CreatePathFinder(map, gameObjects);

            var path = pathFinder
                .GetAllAdjacentPositionsToTile(target, true)
                .ToArray();
            Assert.Equal(
                expectedPoints.OrderBy(x => x.ToString()),
                path.OrderBy(x => x.ToString()));
        }

        // 4 X o o o X
        // 3 o o o o o
        // 2 o o o o o
        // 1 o o o o o 
        // 0 X o o o X
        //   0 1 2 3 4
        [ClassData(typeof(AdjacentToCornersTestData))]
        [Theory]
        private void GetFreeAdjacentPositionsToTile_MapCornersNoColisions_AdjacentTiles(
            Vector2 target,
            IReadOnlyCollection<Vector2> expectedPoints)
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var gameObjects = new IGameObject[] { };
            var pathFinder = CreatePathFinder(map, gameObjects);

            var path = pathFinder
                .GetFreeAdjacentPositionsToTile(target, true)
                .ToArray();
            Assert.Equal(
                expectedPoints.OrderBy(x => x.ToString()),
                path.OrderBy(x => x.ToString()));
        }

        // 4 o o o o o
        // 3 o o o o o
        // 2 o B o o o
        // 1 o A C o o 
        // 0 o o o o o
        //   0 1 2 3 4
        [Fact]
        private void GetAllowedPathDestinations_2AdjacentFullTileObjectsDistance1_ExpectedTiles()
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var gameObjects = new[]
            {
                // A
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 1),
                    new PositionBehavior(1, 1),
                }),
                // B
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 1),
                    new PositionBehavior(1.5, 2.5),
                }),
                // C
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 1),
                    new PositionBehavior(2.5, 1.5),
                }),
            };
            var pathFinder = CreatePathFinder(map, gameObjects);

            var path = pathFinder
                .GetAllowedPathDestinations(
                    new Vector2(1, 1), // at position A
                    new Vector2(1, 1),
                    1,
                    true)
                .ToArray();
            Assert.Equal(
                new[]
                {
                    new Vector2(0, 1),
                    new Vector2(1, 0),
                }.OrderBy(x => x.X).ThenBy(x => x.Y),
                path.OrderBy(x => x.X).ThenBy(x => x.Y));
        }

        // 4 o o o o o
        // 3 o o o o o
        // 2 o B o o o
        // 1 o A C o o 
        // 0 o o o o o
        //   0 1 2 3 4
        [Fact]
        private void GetAllowedPathDestinations_2AdjacentFullTileObjectsDistance2_ExpectedTiles()
        {
            var map = CreateMap(CreateTileGrid(5, 5));
            var gameObjects = new[]
            {
                // A
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 1),
                    new PositionBehavior(1, 1),
                }),
                // B
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 1),
                    new PositionBehavior(1.5, 2.5),
                }),
                // C
                new GameObject(new IBehavior[]
                {
                    new SizeBehavior(1, 1),
                    new PositionBehavior(2.5, 1.5),
                }),
            };
            var pathFinder = CreatePathFinder(map, gameObjects);

            var path = pathFinder
                .GetAllowedPathDestinations(
                    new Vector2(1, 1), // at position A
                    new Vector2(1, 1),
                    2,
                    true)
                .ToArray();
            Assert.Equal(
                new[]
                {
                    new Vector2(0, 1),
                    new Vector2(1, 0),
                    new Vector2(0, 0),
                    new Vector2(2, 0),
                    new Vector2(0, 2),
                    //new Vector2(2, 2), // FIXME: (2,2) should be included but collisions don't currently let us intersect corners to perform diagonal movement
                }.OrderBy(x => x.X).ThenBy(x => x.Y),
                path.OrderBy(x => x.X).ThenBy(x => x.Y));
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
