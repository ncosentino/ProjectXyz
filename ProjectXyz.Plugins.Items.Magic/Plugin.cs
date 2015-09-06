using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;
using ProjectXyz.Data.Sql.Stats;
using ProjectXyz.Plugins.Items.Normal;

namespace ProjectXyz.Plugins.Items.Magic
{
    public sealed class Plugin : IItemPlugin
    {
        #region Fields
        private readonly IMagicItemGenerator _magicItemGenerator;
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

            var normalItemGenerator = NormalItemGenerator.Create(
                applicationManager.Items.ItemFactory,
                applicationManager.Items.ItemMetaDataFactory,
                applicationManager.Items.ItemRequirementsFactory,
                dataManager.Stats.StatFactory,
                statRepository,
                dataManager.Items);

            _magicItemGenerator = MagicItemGenerator.Create(
                MagicTypeId,
                applicationManager.Items.ItemFactory,
                applicationManager.Items.ItemMetaDataFactory,
                normalItemGenerator,
                applicationManager.Items.ItemAffixGenerator,
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
            get { return _magicItemGenerator.Generate; }
        }
        #endregion
    }
}
