namespace ProjectXyz.Api.Framework
{
    public static class IRandomNumberGeneratorExtensions
    {
        public static int NextInRange(
            this IRandomNumberGenerator randomNumberGenerator,
            int minInclusive,
            int maxInclusive)
        {
            var value = minInclusive + (int)System.Math.Round(randomNumberGenerator.NextDouble() * (maxInclusive - minInclusive), 0);
            return value;
        }

        public static double NextInRange(
            this IRandomNumberGenerator randomNumberGenerator,
            double minInclusive,
            double maxInclusive)
        {
            return minInclusive + randomNumberGenerator.NextDouble() * (maxInclusive - minInclusive);
        }
    }
}