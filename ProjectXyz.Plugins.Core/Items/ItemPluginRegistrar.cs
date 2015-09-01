using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Plugins.Items;
using ProjectXyz.Plugins.Interface.Items;

namespace ProjectXyz.Plugins.Core.Items
{
    public sealed class ItemPluginRegistrar : IItemPluginRegistrar
    {
        #region Fields
        private readonly IItemGenerator _itemGenerator;
        #endregion

        #region Constructors
        private ItemPluginRegistrar(IItemGenerator itemGenerator)
        {
            Contract.Requires<ArgumentNullException>(itemGenerator != null);

            _itemGenerator = itemGenerator;
        }
        #endregion

        #region Methods
        public static IItemPluginRegistrar Create(IItemGenerator itemGenerator)
        {
            Contract.Requires<ArgumentNullException>(itemGenerator != null);
            Contract.Ensures(Contract.Result<IItemPluginRegistrar>() != null);

            var registrar = new ItemPluginRegistrar(itemGenerator);
            return registrar;
        }

        public void RegisterPlugins(IEnumerable<IItemPlugin> itemPlugins)
        {
            foreach (var itemPlugin in itemPlugins)
            {
                _itemGenerator.RegisterCallback(
                    itemPlugin.MagicTypeId,
                    itemPlugin.GenerateItemCallback);
            }
        }
        #endregion
    }
}
