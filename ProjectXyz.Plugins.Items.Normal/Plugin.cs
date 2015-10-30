using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql.Stats;

namespace ProjectXyz.Plugins.Items.Normal
{
    public sealed class Plugin : IItemPlugin
    {
        #region Fields
        private readonly IItemTypeGenerator _normalItemGenerator;
        #endregion

        #region Constructors
        public Plugin(
            IDatabase database,
            IDataManager dataManager,
            IStatApplicationManager statApplicationManager,
            IItemApplicationManager itemApplicationManager)
        {
            var statRepository = StatRepository.Create(
                database,
                dataManager.Stats.StatFactory);

            var itemRequirementsGenerator = ItemRequirementsGenerator.Create(
                dataManager.Items.ItemDefinitionStatRequirements,
                dataManager.Stats.Stats,
                dataManager.Items.ItemDefinitionItemMiscRequirements,
                dataManager.Items.ItemMiscRequirements,
                itemApplicationManager.ItemRequirementsFactory);

            _normalItemGenerator = NormalItemGenerator.Create(
                itemApplicationManager.ItemFactory,
                itemApplicationManager.ItemMetaDataFactory,
                itemApplicationManager.ItemRequirementsFactory,
                dataManager.Items.ItemNamePartFactory,
                statRepository,
                dataManager.Items,
                statApplicationManager.StatGenerator,
                itemRequirementsGenerator);
        }
        #endregion

        #region Properties
        public IItemTypeGenerator ItemTypeGenerator
        {
            get { return _normalItemGenerator; }
        }
        #endregion
    }
}
