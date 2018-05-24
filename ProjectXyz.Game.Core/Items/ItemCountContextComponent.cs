using ProjectXyz.Framework.Contracts;

namespace ProjectXyz.Game.Core.Items
{
    public sealed class ItemCountContextComponent : IItemCountContextComponent
    {
        public ItemCountContextComponent(
            int minimum,
            int maximum)
        {
            Contract.Requires(
                minimum <= maximum,
                $"{nameof(minimum)} ({minimum}) must be <= {nameof(maximum)} ({maximum}).");

            Minimum = minimum;
            Maximum = maximum;
        }

        public int Minimum { get; }

        public int Maximum { get; }
    }
}
