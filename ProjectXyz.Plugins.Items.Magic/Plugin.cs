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
            IDataStore dataStore,
            IApplicationManager applicationManager)
        {
            var statRepository = StatRepository.Create(
                database,
                dataStore.Stats.StatFactory);

            var normalItemGenerator = NormalItemGenerator.Create(
                applicationManager.Items.ItemFactory,
                applicationManager.Items.ItemMetaDataFactory,
                applicationManager.Items.ItemRequirementsFactory,
                dataStore.Stats.StatFactory,
                statRepository,
                dataStore.Items);

            _magicItemGenerator = MagicItemGenerator.Create(
                MagicTypeId,
                applicationManager.Items.ItemFactory,
                applicationManager.Items.ItemMetaDataFactory,
                normalItemGenerator,
                applicationManager.Items.ItemAffixGenerator,
                dataStore.Items);
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
