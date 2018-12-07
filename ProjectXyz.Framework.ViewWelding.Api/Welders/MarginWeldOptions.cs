using System;

namespace ProjectXyz.Framework.ViewWelding.Api.Welders
{
    public sealed class MarginWeldOptions : IMarginWeldOptions
    {
        private static readonly Lazy<IMarginWeldOptions> LAZY_NONE = new Lazy<IMarginWeldOptions>(
            () => new MarginWeldOptions(
                0,
                0,
                0,
                0));

        public MarginWeldOptions(
            int leftMargin,
            int rightMargin,
            int topMargin,
            int bottomMargin)
        {
            LeftMargin = leftMargin;
            RightMargin = rightMargin;
            TopMargin = topMargin;
            BottomMargin = bottomMargin;
        }

        public static IMarginWeldOptions None => LAZY_NONE.Value;

        public int LeftMargin { get; }

        public int RightMargin { get; }

        public int TopMargin { get; }

        public int BottomMargin { get; }
    }
}