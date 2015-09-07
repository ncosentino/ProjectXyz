using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql.Stats;
using ProjectXyz.Plugins.Items.Normal;

namespace ProjectXyz.Plugins.Items.Magic
{
    public sealed class Plugin : IItemPlugin
    {
        #region Fields
        private readonly IItemTypeGenerator _magicItemGenerator;
        #endregion

        #region Constructors
        public Plugin(
            IDatabase database, 
            IDataManager dataManager,
            IItemApplicationManager itemApplicationManager)
        {
            var statRepository = StatRepository.Create(
                database,
                dataManager.Stats.StatFactory);

            var normalItemGenerator = NormalItemGenerator.Create(
                itemApplicationManager.ItemFactory,
                itemApplicationManager.ItemMetaDataFactory,
                itemApplicationManager.ItemRequirementsFactory,
                dataManager.Stats.StatFactory,
                statRepository,
                dataManager.Items);

            var itemTypeGeneratorClassName = typeof(MagicItemGenerator).FullName;
            var itemTypeGeneratorPlugin = dataManager.Items.ItemTypeGeneratorPlugins.GetByItemGeneratorClassName(itemTypeGeneratorClassName);

            _magicItemGenerator = MagicItemGenerator.Create(
                itemTypeGeneratorPlugin.MagicTypeId,
                itemApplicationManager.ItemFactory,
                itemApplicationManager.ItemMetaDataFactory,
                normalItemGenerator,
                itemApplicationManager.ItemAffixGenerator,
                dataManager.Items);
        }
        #endregion

        #region Properties
        public IItemTypeGenerator ItemTypeGenerator
        {
            get { return _magicItemGenerator; }
        }
        #endregion
    }
}
