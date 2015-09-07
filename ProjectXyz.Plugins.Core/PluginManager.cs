using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Plugins.Enchantments;
using ProjectXyz.Plugins.Interface;
using ProjectXyz.Plugins.Items;

namespace ProjectXyz.Plugins.Core
{
    public sealed class PluginManager : IPluginManager
    {
        #region Fields
        private readonly IPluginRepository<IEnchantmentPlugin> _enchantmentPluginRepository;
        private readonly IPluginRepository<IItemPlugin> _itemPluginRepository;
        #endregion

        #region Constructors
        private PluginManager(
            IPluginRepository<IEnchantmentPlugin> enchantmentPluginRepository,
            IPluginRepository<IItemPlugin> itemPluginRepository)
        {
            _enchantmentPluginRepository = enchantmentPluginRepository;
            _itemPluginRepository = itemPluginRepository;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IEnumerable<IEnchantmentPlugin> Enchantments { get { return _enchantmentPluginRepository.Plugins; } }

        /// <inheritdoc />
        public IEnumerable<IItemPlugin> Items { get { return _itemPluginRepository.Plugins; } }
        #endregion

        #region Methods
        public static IPluginManager Create(
            IPluginRepository<IEnchantmentPlugin> enchantmentPluginRepository,
            IPluginRepository<IItemPlugin> itemPluginRepository)
        {
            var manager = new PluginManager(
                enchantmentPluginRepository,
                itemPluginRepository);
            return manager;
        }
        #endregion
    }
}
