using System;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class SizeBehavior :
        BaseBehavior,
        ISizeBehavior
    {
        private double _width;
        private double _height;
        private bool _disableEventChange;

        public SizeBehavior(double width, double height)
        {
            SetSize(width, height);
        }

        public SizeBehavior()
            : this(0, 0)
        {
        }

        public event EventHandler<EventArgs> SizeChanged;

        public double Width
        {
            get { return _width; }
            private set
            {
                if (!SetWidthIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    SizeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public double Height
        {
            get { return _height; }
            private set
            {
                if (!SetHeightIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    SizeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void SetSize(double width, double height)
        {
            try
            {
                _disableEventChange = true;
                var changed = SetWidthIfChanged(width);
                changed |= SetHeightIfChanged(height);

                if (changed)
                {
                    SizeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            finally
            {
                _disableEventChange = false;
            }
        }

        private bool SetWidthIfChanged(double value)
        {
            if (Math.Abs(_width - value) < double.Epsilon)
            {
                return false;
            }

            _width = value;
            return true;
        }

        private bool SetHeightIfChanged(double value)
        {
            if (Math.Abs(_height - value) < double.Epsilon)
            {
                return false;
            }

            _height = value;
            return true;
        }
    }
}
