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
        #region Constants
        // FIXME: find a way that we can map this in the database instead...
        private static readonly Guid MAGIC_TYPE = Guid.NewGuid();
        #endregion

        #region Fields
        private readonly IMagicItemGenerator _magicItemGenerator;
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

            _magicItemGenerator = MagicItemGenerator.Create(
                MagicTypeId,
                itemApplicationManager.ItemFactory,
                itemApplicationManager.ItemMetaDataFactory,
                normalItemGenerator,
                itemApplicationManager.ItemAffixGenerator,
                dataManager.Items);
        }
        #endregion

        #region Properties
        public Guid MagicTypeId
        {
            get { return MAGIC_TYPE; }
        }

        public GenerateItemDelegate GenerateItemCallback
        {
            get { return _magicItemGenerator.Generate; }
        }
        #endregion
    }
}
