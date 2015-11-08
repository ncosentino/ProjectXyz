using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Core.Shading;
using ProjectXyz.Application.Interface.GameObjects;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Application.Interface.Shading;
using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Application.Core.Maps
{
    public sealed class Map : IMap
    {
        #region Fields
        private readonly IMapContext _context;
        private readonly IMapStore _store;
        private readonly IMutableShade _shade;
        #endregion

        #region Constructors
        private Map(IMapContext context, IMapStore mapStore)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(mapStore != null);

            _context = context;
            _store = mapStore;

            _shade = Shade.Create(1f, 1f, 1f, 1f);
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _store.Id; }
        }

        public string ResourceName
        {
            get { return "Assets/Resources/Maps/Swamp.tmx"; }
        }

        public bool IsInterior
        {
            get { throw new NotImplementedException(); }
        }

        public IObservableShade AmbientLight
        {
            get { return _shade; }
        }
        #endregion

        #region Methods
        public static IMap Create(IMapContext context, IMapStore mapStore)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(mapStore != null);

            Contract.Ensures(Contract.Result<IMap>() != null);
            return new Map(context, mapStore);
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            // TODO: do some time-of-day stuff with the context?

            float brightness;
            if (_context.Calendar.DateTime.Minutes >= 30)
            {
                brightness = (60 - _context.Calendar.DateTime.Minutes) / 30f;
            }
            else
            {
                brightness = _context.Calendar.DateTime.Minutes / 30f;
            }

            _shade.Red = brightness;
            _shade.Green = brightness;
            _shade.Blue = brightness;
            _shade.Alpha = 1f;
        }

        public TGameObject FindGameObject<TGameObject>(Guid id)
            where TGameObject : IGameObject
        {
            // TODO: implement this
            return default(TGameObject);
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_context != null);
            Contract.Invariant(_store != null);
            Contract.Invariant(_shade != null);
        }
        #endregion
    }
}
