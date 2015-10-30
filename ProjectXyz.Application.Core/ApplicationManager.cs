using System;
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
using ProjectXyz.Application.Interface.Stats;
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
        private readonly IStatApplicationManager _statApplicationManager;
        #endregion

        #region Constructors
        private ApplicationManager(
            IDataManager dataManager,
            IEnchantmentApplicationFactoryManager enchantmentApplicationFactoryManager,
            IStatApplicationManager statApplicationManager,
            IItemApplicationManager itemApplicationManager)
        {
            Contract.Requires<ArgumentNullException>(dataManager != null);
            Contract.Requires<ArgumentNullException>(enchantmentApplicationFactoryManager != null);
            Contract.Requires<ArgumentNullException>(statApplicationManager != null);
            Contract.Requires<ArgumentNullException>(itemApplicationManager != null);

            _statApplicationManager = statApplicationManager;

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
        /// <inheritdoc />
        public IActorManager Actors
        {
            get { return _actorManager; }
        }

        /// <inheritdoc />
        public IMapManager Maps
        {
            get { return _mapManager; }
        }

        /// <inheritdoc />
        public IItemApplicationManager Items
        {
            get { return _itemApplicationManager; }
        }

        /// <inheritdoc />
        public IEnchantmentApplicationManager Enchantments
        {
            get { return _enchantmentManager; }
        }

        /// <inheritdoc />
        public IStatApplicationManager Stats
        {
            get { return _statApplicationManager; }
        }
        #endregion

        #region Methods
        public static IApplicationManager Create(
            IDataManager dataManager,
            IEnchantmentApplicationFactoryManager enchantmentApplicationFactoryManager,
            IStatApplicationManager statApplicationManager,
            IItemApplicationManager itemApplicationManager)
        {
            Contract.Requires<ArgumentNullException>(dataManager != null);
            Contract.Requires<ArgumentNullException>(enchantmentApplicationFactoryManager != null);
            Contract.Requires<ArgumentNullException>(statApplicationManager != null);
            Contract.Requires<ArgumentNullException>(itemApplicationManager != null);
            Contract.Ensures(Contract.Result<IApplicationManager>() != null);

            return new ApplicationManager(
                dataManager, 
                enchantmentApplicationFactoryManager,
                statApplicationManager,
                itemApplicationManager);
        }
        #endregion
    }
}
