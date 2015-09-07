using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
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
            IItemApplicationManager itemApplicationManager)
        {
            var statRepository = StatRepository.Create(
                database,
                dataManager.Stats.StatFactory);

            _normalItemGenerator = NormalItemGenerator.Create(
                itemApplicationManager.ItemFactory,
                itemApplicationManager.ItemMetaDataFactory,
                itemApplicationManager.ItemRequirementsFactory,
                dataManager.Stats.StatFactory,
                statRepository,
                dataManager.Items);
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
