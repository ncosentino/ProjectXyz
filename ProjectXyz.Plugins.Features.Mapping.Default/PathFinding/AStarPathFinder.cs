using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default.PathFinding
{
    public sealed class AStarPathFinder : IPathFinder
    {
        private readonly IPathFinderCollisionDetector _collisionDetector;
        private readonly Dictionary<int, Dictionary<int, IGameObject>> _grid;

        public AStarPathFinder(
            IGameObject map,
            IPathFinderCollisionDetector collisionDetector)
        {
            _collisionDetector = collisionDetector;
            _grid = new Dictionary<int, Dictionary<int, IGameObject>>();
            foreach (var tile in map.GetOnly<IMapLayersBehavior>().Layers.First().Tiles)
            {
                var tilePositionBehavior = tile.GetOnly<IReadOnlyPositionBehavior>();
                if (!_grid.TryGetValue((int)tilePositionBehavior.X, out var rows))
                {
                    rows = new Dictionary<int, IGameObject>();
                    _grid[(int)tilePositionBehavior.X] = rows;
                }

                rows[(int)tilePositionBehavior.Y] = tile;
            }
        }

        public IEnumerable<IGameObject> GetIntersectingGameObjects(
            Vector2 position,
            Vector2 size)
        {
            _collisionDetector.Reset(new Vector4[0]);
            var area = new Vector4(
                position.X - size.X / 2,
                position.Y - size.Y / 2,
                position.X + size.X / 2,
                position.Y + size.Y / 2);
            return _collisionDetector.GameObjectsIntersectingArea(area);
        }

        public async Task<IPath> FindPathAsync(
            Vector2 startPosition,
            Vector2 endPosition,
            Vector2 size,
            bool includeDiagonals)
        {
            var startNode = new Node(ToTilePosition(startPosition));
            var endNode = new Node(ToTilePosition(endPosition));

            // we want to make sure we dont collide with the starting position rectangle
            _collisionDetector.Reset(new[]
            {
                new Vector4(
                    startPosition.X - size.X / 2,
                    startPosition.Y - size.Y / 2,
                    startPosition.X + size.X / 2,
                    startPosition.Y + size.Y / 2),
            });

            var openList = new List<Node>();
            var openListPositions = new HashSet<Vector2>();
            var closedListPositions = new HashSet<Vector2>();
            var currentNode = startNode;

            // add start node to Open List
            openList.Add(startNode);
            openListPositions.Add(startNode.Position);

            while (openList.Count != 0 && !closedListPositions.Contains(endNode.Position))
            {
                currentNode = openList[0];

                openList.Remove(currentNode);
                openListPositions.Remove(currentNode.Position);

                closedListPositions.Add(currentNode.Position);

                foreach (var adjacentPosition in GetAdjacentPositions(
                    currentNode.Position,
                    includeDiagonals: includeDiagonals,
                    checkCollisions: true,
                    collidersToIgnore: null))
                {
                    await Task.Yield();

                    if (closedListPositions.Contains(adjacentPosition))
                    {
                        continue;
                    }

                    if (openListPositions.Contains(adjacentPosition))
                    {
                        continue;
                    }

                    var distanceToTarget =
                        Math.Abs(adjacentPosition.X - endNode.Position.X) +
                        Math.Abs(adjacentPosition.Y - endNode.Position.Y);
                    var weight = (float)Math.Sqrt(
                        Math.Pow(adjacentPosition.X - currentNode.Position.X, 2) +
                        Math.Pow(adjacentPosition.Y - currentNode.Position.Y, 2));

                    var adjacentNode = new Node(
                        adjacentPosition,
                        currentNode,
                        distanceToTarget,
                        weight);

                    openList.Add(adjacentNode);
                    openList = openList.OrderBy(node => node.F).ToList();
                    openListPositions.Add(adjacentNode.Position);
                }
            }

            // construct path, if end was not closed return null
            if (!closedListPositions.Contains(endNode.Position))
            {
                return Path.Empty;
            }

            // the ol' reversy
            var returnStack = new Stack<Node>();
            while (currentNode != startNode && currentNode != null)
            {
                returnStack.Push(currentNode);
                currentNode = currentNode.Parent;
            }

            var path = new Path(returnStack.Select(n => new KeyValuePair<Vector2, double>(n.Position, n.Cost)));
            return path;
        }

        public IEnumerable<Vector2> GetAllowedPathDestinations(
            Vector2 position,
            Vector2 size,
            double maximumDistance,
            bool includeDiagonals)
        {
            var startNode = new Node(ToTilePosition(position));

            // we want to make sure we dont collide with the starting position rectangle
            _collisionDetector.Reset(new[]
            {
                new Vector4(
                    position.X - size.X / 2,
                    position.Y - size.Y / 2,
                    position.X + size.X / 2,
                    position.Y + size.Y / 2),
            });

            var openList = new List<Node>();
            var openListPositions = new HashSet<Vector2>();
            var closedListPositions = new HashSet<Vector2>();

            // add start node to Open List
            openList.Add(startNode);
            openListPositions.Add(startNode.Position);

            while (openList.Count != 0)
            {
                var currentNode = openList[0];

                openList.Remove(currentNode);
                openListPositions.Remove(currentNode.Position);
                closedListPositions.Add(currentNode.Position);

                foreach (var adjacentPosition in GetAdjacentPositions(
                    currentNode.Position,
                    includeDiagonals: includeDiagonals,
                    checkCollisions: true,
                    collidersToIgnore: null))
                {
                    if (closedListPositions.Contains(adjacentPosition))
                    {
                        continue;
                    }

                    if (openListPositions.Contains(adjacentPosition))
                    {
                        continue;
                    }

                    var weight = (float)Math.Sqrt(
                        Math.Pow(adjacentPosition.X - currentNode.Position.X, 2) +
                        Math.Pow(adjacentPosition.Y - currentNode.Position.Y, 2));
                    var adjacentNode = new Node(
                        adjacentPosition,
                        currentNode,
                        0f,
                        weight);
                    if (adjacentNode.Cost > maximumDistance)
                    {
                        continue;
                    }

                    openList.Add(adjacentNode);
                    openListPositions.Add(adjacentNode.Position);
                    yield return adjacentNode.Position;
                }
            }
        }

        public IEnumerable<Vector2> GetAllAdjacentPositionsToTile(
            Vector2 position,
            bool includeDiagonals) => GetAdjacentPositions(
                ToTilePosition(position),
                includeDiagonals,
                false,
                null);

        public IEnumerable<Vector2> GetFreeAdjacentPositionsToTile(
            Vector2 position,
            bool includeDiagonals) => GetAdjacentPositions(
                ToTilePosition(position),
                includeDiagonals,
                true,
                null);

        public IEnumerable<Vector2> GetAllAdjacentPositionsToObject(
            Vector2 position,
            Vector2 size,
            bool includeDiagonals) => GetAdjacentPositions(
                ToTilePosition(position),
                includeDiagonals,
                false,
                new[] { ToRect(position, size) });

        public IEnumerable<Vector2> GetFreeAdjacentPositionsToObject(
            Vector2 position,
            Vector2 size,
            bool includeDiagonals) => GetAdjacentPositions(
                ToTilePosition(position),
                includeDiagonals,
                true,
                new[] { ToRect(position, size) });

        private IEnumerable<Vector2> GetAdjacentPositions(
            Vector2 position,
            bool includeDiagonals,
            bool checkCollisions,
            IEnumerable<Vector4> collidersToIgnore = null)
        {
            var tilePosition = ToTilePosition(position);

            if (checkCollisions && collidersToIgnore != null)
            {
                _collisionDetector.Reset(collidersToIgnore);
            }

            var adjacentVectors = new List<Vector2>()
            {
                new Vector2(tilePosition.X, tilePosition.Y + 1),
                new Vector2(tilePosition.X, tilePosition.Y - 1),
                new Vector2(tilePosition.X + 1, tilePosition.Y),
                new Vector2(tilePosition.X - 1, tilePosition.Y),
            };
            if (includeDiagonals)
            {
                adjacentVectors.AddRange(new[]
                {
                    new Vector2(tilePosition.X + 1, tilePosition.Y + 1),
                    new Vector2(tilePosition.X - 1, tilePosition.Y - 1),
                    new Vector2(tilePosition.X - 1, tilePosition.Y + 1),
                    new Vector2(tilePosition.X + 1, tilePosition.Y - 1),
                });
            }

            foreach (var adjacentVector in adjacentVectors)
            {
                if (!TryGetTile((int)adjacentVector.X, (int)adjacentVector.Y, out var tile))
                {
                    continue;
                }

                if (checkCollisions && _collisionDetector.CollisionsAlongPath(
                    tilePosition,
                    adjacentVector))
                {
                    continue;
                }

                yield return adjacentVector;
            }
        }

        private Vector2 ToTilePosition(Vector2 position)
        {
            // ensure integer-based
            var tilePosition = new Vector2(
                (int)Math.Round(position.X),
                (int)Math.Round(position.Y));
            return tilePosition;
        }

        private Vector4 ToRect(Vector2 position, Vector2 size)
        {
            var rect = new Vector4(
                position.X - size.X / 2,
                position.Y - size.Y / 2,
                position.X + size.X / 2,
                position.Y + size.Y / 2);
            return rect;
        }

        private bool TryGetTile(int x, int y, out IGameObject tile)
        {
            tile = null;

            if (!_grid.TryGetValue(x, out var rows))
            {
                return false;
            }

            if (!rows.TryGetValue(y, out tile))
            {
                return false;
            }

            return true;
        }

        private sealed class Node
        {
            public Node(Vector2 position)
                : this(position, null, -1, 0)
            {
            }

            public Node(Vector2 pos, Node parent, float distanceToTarget, float weight)
            {
                Parent = parent;
                Position = pos;
                DistanceToTarget = distanceToTarget;
                Cost = weight + (parent == null ? 0 : parent.Cost);
            }

            public Node Parent { get; }

            public Vector2 Position { get; }

            public float DistanceToTarget { get; }

            public float Cost { get; }

            public float F
            {
                get
                {
                    return DistanceToTarget != -1 && Cost != -1
                        ? DistanceToTarget + Cost
                        : -1;
                }
            }
        }
    }
}