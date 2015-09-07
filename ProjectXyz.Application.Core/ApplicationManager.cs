using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Maps;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Application.Core
{
    public sealed class ApplicationManager : IApplicationManager
    {
        #region Fields
        private readonly IActorManager _actorManager;
        private readonly IMapManager _mapManager;
        private readonly IItemApplicationManager _itemApplicationManager;
        private readonly IEnchantmentApplicationManager _enchantmentManager;
        #endregion

        #region Constructors
        private ApplicationManager(
            IDataManager dataManager,
            IEnchantmentApplicationFactoryManager enchantmentApplicationFactoryManager,
            IItemApplicationManager itemApplicationManager)
        {
            Contract.Requires<ArgumentNullException>(dataManager != null);
            Contract.Requires<ArgumentNullException>(enchantmentApplicationFactoryManager != null);
            Contract.Requires<ArgumentNullException>(itemApplicationManager != null);

            _actorManager = ActorManager.Create(dataManager.Actors);
            
            _mapManager = MapManager.Create(dataManager.Maps);

            // FIXME: initialize these...
            var enchantmentTypeCalculators = Enumerable.Empty<IEnchantmentTypeCalculator>();
            _enchantmentManager = EnchantmentApplicationManager.Create(
                dataManager,
                enchantmentApplicationFactoryManager,
                enchantmentTypeCalculators);

            _itemApplicationManager = itemApplicationManager;
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

        public IItemApplicationManager Items
        {
            get { return _itemApplicationManager; }
        }

        public IEnchantmentApplicationManager Enchantments
        {
            get { return _enchantmentManager; }
        }
        #endregion

        #region Methods
        public static IApplicationManager Create(
            IDataManager dataManager,
            IEnchantmentApplicationFactoryManager enchantmentApplicationFactoryManager,
            IItemApplicationManager itemApplicationManager)
        {
            Contract.Requires<ArgumentNullException>(dataManager != null);
            Contract.Requires<ArgumentNullException>(enchantmentApplicationFactoryManager != null);
            Contract.Requires<ArgumentNullException>(itemApplicationManager != null);
            Contract.Ensures(Contract.Result<IApplicationManager>() != null);

            return new ApplicationManager(
                dataManager, 
                enchantmentApplicationFactoryManager,
                itemApplicationManager);
        }
        #endregion
    }
}
