namespace ProjectXyz.Framework.Interface
{
    public static class IRandomNumberGeneratorExtensions
    {
        public static int NextInRange(
            this IRandomNumberGenerator randomNumberGenerator,
            int minInclusive,
            int maxInclusive)
        {
            return minInclusive + (int)System.Math.Round(randomNumberGenerator.NextDouble() * ((maxInclusive + 1) - minInclusive));
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