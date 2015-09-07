using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Plugins.Items;
using ProjectXyz.Plugins.Interface.Items;

namespace ProjectXyz.Plugins.Core.Items
{
    public sealed class ItemPluginRegistrar : IItemPluginRegistrar
    {
        #region Fields
        private readonly IItemGenerator _itemGenerator;
        private readonly IItemTypeGeneratorPluginRepository _itemTypeGeneratorPluginRepository;
        #endregion

        #region Constructors
        private ItemPluginRegistrar(
            IItemTypeGeneratorPluginRepository itemTypeGeneratorPluginRepository,
            IItemGenerator itemGenerator)
        {
            Contract.Requires<ArgumentNullException>(itemTypeGeneratorPluginRepository != null);
            Contract.Requires<ArgumentNullException>(itemGenerator != null);

            _itemTypeGeneratorPluginRepository = itemTypeGeneratorPluginRepository;
            _itemGenerator = itemGenerator;
        }
        #endregion

        #region Methods
        public static IItemPluginRegistrar Create(
            IItemTypeGeneratorPluginRepository itemTypeGeneratorPluginRepository,
            IItemGenerator itemGenerator)
        {
            Contract.Requires<ArgumentNullException>(itemTypeGeneratorPluginRepository != null);
            Contract.Requires<ArgumentNullException>(itemGenerator != null);
            Contract.Ensures(Contract.Result<IItemPluginRegistrar>() != null);

            var registrar = new ItemPluginRegistrar(
                itemTypeGeneratorPluginRepository,
                itemGenerator);
            return registrar;
        }

        public void RegisterPlugins(IEnumerable<IItemPlugin> itemPlugins)
        {
            foreach (var itemPlugin in itemPlugins)
            {
                var itemGeneratorClassName = itemPlugin.ItemTypeGenerator.GetType().FullName;
                var itemTypeGeneratorPlugin = _itemTypeGeneratorPluginRepository.GetByItemGeneratorClassName(itemGeneratorClassName);

                _itemGenerator.RegisterCallback(
                    itemTypeGeneratorPlugin.MagicTypeId,
                    itemPlugin.ItemTypeGenerator);
            }
        }
        #endregion
    }
}
