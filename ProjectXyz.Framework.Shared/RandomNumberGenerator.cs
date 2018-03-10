using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Shared.Framework
{
    public sealed class RandomNumberGenerator : IRandomNumberGenerator
    {
        #region Fields
        private readonly System.Random _random;
        #endregion

        #region Constructors
        public RandomNumberGenerator(System.Random random)
        {
            _random = random;
        }
        #endregion

        #region Methods
        public double NextDouble()
        {
            return _random.NextDouble();
        }
        #endregion
    }
}