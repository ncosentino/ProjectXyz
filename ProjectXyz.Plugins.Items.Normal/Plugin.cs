using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;
using ProjectXyz.Data.Sql.Stats;

namespace ProjectXyz.Plugins.Items.Normal
{
    public sealed class Plugin : IItemPlugin
    {
        #region Fields
        private readonly INormalItemGenerator _normalItemGenerator;
        #endregion

        #region Constructors
        public Plugin(
            IDatabase database,
            IDataManager dataManager,
            IApplicationManager applicationManager)
        {
            var statRepository = StatRepository.Create(
                database,
                dataManager.Stats.StatFactory);

            _normalItemGenerator = NormalItemGenerator.Create(
                applicationManager.Items.ItemFactory,
                applicationManager.Items.ItemMetaDataFactory,
                applicationManager.Items.ItemRequirementsFactory,
                dataManager.Stats.StatFactory,
                statRepository,
                dataManager.Items);
        }
        #endregion

        #region Properties
        public Guid MagicTypeId
        {
            get { throw new NotImplementedException(); }
        }

        public GenerateItemDelegate GenerateItemCallback
        {
            get { return _normalItemGenerator.Generate; }
        }
        #endregion
    }
}
