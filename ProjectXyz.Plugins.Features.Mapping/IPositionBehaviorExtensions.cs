using System.Numerics;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public static class IPositionBehaviorExtensions
    {
        public static Vector2 GetPosition(this IReadOnlyPositionBehavior positionBehavior)
        {
            var positionVector = new Vector2(
                (float)positionBehavior.X,
                (float)positionBehavior.Y);
            return positionVector;
        }

        public static void SetPosition(
            this IPositionBehavior positionBehavior,
            Vector2 positionVector)
        {
            positionBehavior.SetPosition(
                positionVector.X,
                positionVector.Y);
        }
    }
}