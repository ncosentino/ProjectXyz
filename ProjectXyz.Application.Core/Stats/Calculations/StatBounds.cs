using ProjectXyz.Application.Interface.Stats.Calculations;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatBounds : IStatBounds
    {
        #region Constructors
        public StatBounds(
            string minimumExpression,
            string maximumExpression)
        {
            MinimumExpression = minimumExpression;
            MaximumExpression = maximumExpression;
        }
        #endregion

        #region Properties
        public string MinimumExpression { get; }

        public string MaximumExpression { get; }
        #endregion

        #region Methods
        public static IStatBounds Max(string maximumExpression)
        {
            return new StatBounds(null, maximumExpression);
        }

        public static IStatBounds Min(string minimumExpression)
        {
            return new StatBounds(minimumExpression, null);
        }
        #endregion
    }
}