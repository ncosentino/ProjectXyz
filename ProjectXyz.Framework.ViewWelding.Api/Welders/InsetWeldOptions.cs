using System;

namespace ProjectXyz.Framework.ViewWelding.Api.Welders
{
    public sealed class InsetWeldOptions : IInsetWeldOptions
    {
        private static readonly Lazy<IInsetWeldOptions> LAZY_NONE = new Lazy<IInsetWeldOptions>(
            () => new InsetWeldOptions(
                0,
                0,
                0,
                0));

        public InsetWeldOptions(int allMargins)
            : this(
                allMargins,
                allMargins,
                allMargins,
                allMargins)
        {
        }

        public InsetWeldOptions(
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

        public static IInsetWeldOptions None => LAZY_NONE.Value;

        public int LeftMargin { get; }

        public int RightMargin { get; }

        public int TopMargin { get; }

        public int BottomMargin { get; }
    }
}