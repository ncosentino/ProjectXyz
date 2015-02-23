using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Shading;

namespace ProjectXyz.Application.Core.Shading
{
    public class Shade : IMutableShade
    {
        #region Fields
        private float _red;
        private float _green;
        private float _blue;
        private float _alpha;
        #endregion

        #region Constructors
        private Shade(float red, float green, float blue, float alpha)
        {
            Contract.Requires<ArgumentOutOfRangeException>(red >= 0 && red <= 1);
            Contract.Requires<ArgumentOutOfRangeException>(green >= 0 && green <= 1);
            Contract.Requires<ArgumentOutOfRangeException>(blue >= 0 && blue <= 1);
            Contract.Requires<ArgumentOutOfRangeException>(alpha >= 0 && alpha <= 1);

            _red = red;
            _green = green;
            _blue = blue;
            _alpha = alpha;
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> ShadeChanged;
        #endregion

        #region Properties
        public float Red
        {
            get
            {
                return _red;
            }

            set
            {
                if (_red == value)
                {
                    return;
                }

                _red = value;
                OnShadeChanged();
            }
        }

        public float Blue
        {
            get
            {
                return _blue;
            }

            set
            {
                if (_blue == value)
                {
                    return;
                }

                _blue = value;
                OnShadeChanged();
            }
        }

        public float Green
        {
            get
            {
                return _green;
            }

            set
            {
                if (_green == value)
                {
                    return;
                }

                _green = value;
                OnShadeChanged();
            }
        }

        public float Alpha
        {
            get
            {
                return _alpha;
            }

            set
            {
                if (_alpha == value)
                {
                    return;
                }

                _alpha = value;
                OnShadeChanged();
            }
        }
        #endregion

        #region Methods
        public static IMutableShade Create(float red, float green, float blue, float alpha)
        {
            Contract.Requires<ArgumentOutOfRangeException>(red >= 0 && red <= 1);
            Contract.Requires<ArgumentOutOfRangeException>(green >= 0 && green <= 1);
            Contract.Requires<ArgumentOutOfRangeException>(blue >= 0 && blue <= 1);
            Contract.Requires<ArgumentOutOfRangeException>(alpha >= 0 && alpha <= 1);
            Contract.Ensures(Contract.Result<IMutableShade>() != null);

            return new Shade(red, green, blue, alpha);
        }

        private void OnShadeChanged()
        {
            var handler = ShadeChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_red >= 0 && _red <= 1);
            Contract.Invariant(_green >= 0 && _green <= 1);
            Contract.Invariant(_blue >= 0 && _blue <= 1);
            Contract.Invariant(_alpha >= 0 && _alpha <= 1);
        }
        #endregion
    }
}
