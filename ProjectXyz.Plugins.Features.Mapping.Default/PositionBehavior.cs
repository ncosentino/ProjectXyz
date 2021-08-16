using System;

using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class PositionBehavior :
        BaseBehavior,
        IPositionBehavior
    {
        private double _x;
        private double _y;
        private bool _disableEventChange;

        public PositionBehavior(double x, double y)
        {
            SetPosition(x, y);
        }

        public PositionBehavior()
            : this(0, 0)
        {
        }

        public event EventHandler<EventArgs> PositionChanged;

        public double X
        {
            get { return _x; }
            private set
            {
                if (!SetXIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    PositionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public double Y
        {
            get { return _y; }
            private set
            {
                if (!SetYIfChanged(value))
                {
                    return;
                }

                if (!_disableEventChange)
                {
                    PositionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void SetPosition(double x, double y)
        {
            try
            {
                _disableEventChange = true;
                var changed = SetXIfChanged(x);
                changed |= SetYIfChanged(y);

                if (changed)
                {
                    PositionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            finally
            {
                _disableEventChange = false;
            }
        }

        private bool SetXIfChanged(double value)
        {
            if (Math.Abs(_x - value) < double.Epsilon)
            {
                return false;
            }

            _x = value;
            return true;
        }

        private bool SetYIfChanged(double value)
        {
            if (Math.Abs(_y - value) < double.Epsilon)
            {
                return false;
            }

            _y = value;
            return true;
        }
    }
}
