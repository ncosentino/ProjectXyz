using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Core.Maps;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Application.Core
{
    public sealed class Manager : IManager
    {
        #region Fields
        private readonly IActorManager _actorManager;
        private readonly IMapManager _mapManager;
        private readonly IItemManager _itemManager;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Manager" /> class.
        /// </summary>
        /// <param name="dataStore">The data store. Cannot be null.</param>
        private Manager(IDataStore dataStore)
        {
            Contract.Requires<ArgumentNullException>(dataStore != null, "dataStore");

            _actorManager = ActorManager.Create(dataStore.Actors);
            _mapManager = MapManager.Create(dataStore.Maps);
            _itemManager = ItemManager.Create();
        }
        #endregion

        #region Properties
        public IActorManager Actors
        {
            get { return _actorManager; }
        }

        public IMapManager Maps
        {
            get { return _mapManager; }
        }

        public IItemManager Items
        {
            get { return _itemManager; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new <see cref="IManager" /> instance.
        /// </summary>
        /// <param name="dataStore">The data store. Cannot be null.</param>
        /// <returns>
        /// Returns a new <see cref="IManager" /> instance.
        /// </returns>
        public static IManager Create(IDataStore dataStore)
        {
            Contract.Requires<ArgumentNullException>(dataStore != null, "dataStore");
            Contract.Ensures(Contract.Result<IManager>() != null);

            return new Manager(dataStore);
        }
        #endregion
    }
}
