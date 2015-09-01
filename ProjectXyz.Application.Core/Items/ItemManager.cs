using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemApplicationManager : IItemApplicationManager
    {
        #region Fields
        private readonly IItemFactory _itemFactory;
        private readonly IItemMetaDataFactory _itemMetaDataFactory;
        private readonly IItemRequirementsFactory _itemRequirementsFactory;
        private readonly IItemAffixFactory _itemAffixFactory;
        private readonly IItemAffixGenerator _itemAffixGenerator;
        #endregion

        #region Constructors
        private ItemApplicationManager(
            IDataStore dataStore,
            IEnchantmentApplicationManager enchantmentApplicationManager)
        {
            _itemFactory = Items.ItemFactory.Create();
            _itemMetaDataFactory = Items.ItemMetaDataFactory.Create();
            _itemRequirementsFactory = Requirements.ItemRequirementsFactory.Create();
            _itemAffixFactory = Affixes.ItemAffixFactory.Create();
            _itemAffixGenerator = Affixes.ItemAffixGenerator.Create(
                enchantmentApplicationManager.EnchantmentGenerator,
                dataStore.Items.ItemAffixDefinitions,
                dataStore.Items.ItemAffixDefinitionFilter,
                dataStore.Items.ItemAffixDefinitionEnchantment,
                _itemAffixFactory,
                dataStore.Items.MagicTypesRandomAffixes);
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IItemFactory ItemFactory
        {
            get { return _itemFactory; }
        }

        /// <inheritdoc />
        public IItemMetaDataFactory ItemMetaDataFactory
        {
            get { return _itemMetaDataFactory; }
        }

        /// <inheritdoc />
        public IItemRequirementsFactory ItemRequirementsFactory
        {
            get { return _itemRequirementsFactory; }
        }

        /// <inheritdoc />
        public IItemAffixFactory ItemAffixFactory
        {
            get { return _itemAffixFactory; }
        }

        /// <inheritdoc />
        public IItemAffixGenerator ItemAffixGenerator
        {
            get { return _itemAffixGenerator; }
        }
        #endregion

        #region Methods
        public static IItemApplicationManager Create(
            IDataStore dataStore,
            IEnchantmentApplicationManager enchantmentApplicationManager)
        {
            Contract.Ensures(Contract.Result<IItemApplicationManager>() != null);

            return new ItemApplicationManager(
                dataStore,
                enchantmentApplicationManager);
        }
        #endregion
    }
}
