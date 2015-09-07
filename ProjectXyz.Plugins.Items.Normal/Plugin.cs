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
        #region Constants
        // FIXME: find a way that we can map this in the database instead...
        private static readonly Guid MAGIC_TYPE = Guid.NewGuid();
        #endregion

        #region Fields
        private readonly INormalItemGenerator _normalItemGenerator;
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
        public Guid MagicTypeId
        {
            get { return MAGIC_TYPE; }
        }

        public GenerateItemDelegate GenerateItemCallback
        {
            get { return _normalItemGenerator.Generate; }
        }
        #endregion
    }
}
