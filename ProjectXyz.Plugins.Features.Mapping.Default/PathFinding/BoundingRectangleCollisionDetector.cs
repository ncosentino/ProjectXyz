using System;
using System.Collections.Generic;
using System.Numerics;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default.PathFinding
{
    public sealed class BoundingRectangleCollisionDetector : IPathFinderCollisionDetector
    {
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly Dictionary<IGameObject, Vector4> _collisionBoxes;

        public BoundingRectangleCollisionDetector(IReadOnlyMapGameObjectManager mapGameObjectManager)
        {
            _mapGameObjectManager = mapGameObjectManager;
            _collisionBoxes = new Dictionary<IGameObject, Vector4>();
        }

        public void Reset(IEnumerable<Vector4> collidersToIgnore)
        {
            var ignoreLookup = new HashSet<Vector4>(collidersToIgnore);
            _collisionBoxes.Clear();

            foreach (var gameObject in _mapGameObjectManager.GameObjects)
            {
                var positionBehavior = gameObject.GetOnly<IPositionBehavior>();
                var sizeBehavior = gameObject.GetOnly<ISizeBehavior>();
                var collisionBox = new Vector4(
                    (float)(positionBehavior.X - sizeBehavior.Width / 2),
                    (float)(positionBehavior.Y - sizeBehavior.Height / 2),
                    (float)(positionBehavior.X + sizeBehavior.Width / 2),
                    (float)(positionBehavior.Y + sizeBehavior.Height / 2));
                if (ignoreLookup.Contains(collisionBox))
                {
                    continue;
                }

                _collisionBoxes[gameObject] = collisionBox;
            }
        }

        public bool CollisionsAlongPath(Vector2 source, Vector2 target)
        {
            foreach (var collisionBox in _collisionBoxes.Values)
            {
                // FIXME: this assumes we can't have things like walls that are on an angle... ew.
                if (SegmentIntersectRectangle(
                    collisionBox.X, // x-min
                    collisionBox.Y, // y-min
                    collisionBox.Z, // x-max
                    collisionBox.W, // y-max
                    source.X,
                    source.Y,
                    target.X,
                    target.Y))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<IGameObject> GameObjectsIntersectingArea(Vector4 area)
        {
            foreach (var entry in _collisionBoxes)
            {
                if (RectanglesIntersect(
                    entry.Value.X, // x-min
                    entry.Value.Y, // y-min
                    entry.Value.Z, // x-max
                    entry.Value.W, // y-max
                    area.X,
                    area.Y,
                    area.Z,
                    area.W))
                {
                    yield return entry.Key;
                }
            }
        }

        private static bool RectanglesIntersect(
            double rectangle1MinX,
            double rectangle1MinY,
            double rectangle1MaxX,
            double rectangle1MaxY,
            double rectangle2MinX,
            double rectangle2MinY,
            double rectangle2MaxX,
            double rectangle2MaxY)
        {
            return 
                (rectangle2MaxX >= rectangle1MinX && rectangle2MinX <= rectangle1MaxX) &&
                (rectangle2MaxY >= rectangle1MinY && rectangle2MinY <= rectangle1MaxY);
        }


        private static bool SegmentIntersectRectangle(
            double rectangleMinX,
            double rectangleMinY,
            double rectangleMaxX,
            double rectangleMaxY,
            double p1X,
            double p1Y,
            double p2X,
            double p2Y)
        {
            // Find min and max X for the segment
            double minX = p1X;
            double maxX = p2X;

            if (p1X > p2X)
            {
                minX = p2X;
                maxX = p1X;
            }

            // Find the intersection of the segment's and rectangle's x-projections
            if (maxX > rectangleMaxX)
            {
                maxX = rectangleMaxX;
            }

            if (minX < rectangleMinX)
            {
                minX = rectangleMinX;
            }

            if (minX > maxX) // If their projections do not intersect return false
            {
                return false;
            }

            // Find corresponding min and max Y for min and max X we found before
            double minY = p1Y;
            double maxY = p2Y;

            double dx = p2X - p1X;

            if (Math.Abs(dx) > double.Epsilon)
            {
                double a = (p2Y - p1Y) / dx;
                double b = p1Y - a * p1X;
                minY = a * minX + b;
                maxY = a * maxX + b;
            }

            if (minY > maxY)
            {
                double tmp = maxY;
                maxY = minY;
                minY = tmp;
            }

            // Find the intersection of the segment's and rectangle's y-projections
            if (maxY > rectangleMaxY)
            {
                maxY = rectangleMaxY;
            }

            if (minY < rectangleMinY)
            {
                minY = rectangleMinY;
            }

            if (minY > maxY) // If Y-projections do not intersect return false
            {
                return false;
            }

            return true;
        }
    }
}