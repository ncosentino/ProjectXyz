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

            var itemStatGenerator = ItemStatGenerator.Create(dataManager.Stats.StatFactory);

            var itemRequirementsGenerator = ItemRequirementsGenerator.Create(
                dataManager.Items.ItemDefinitionStatRequirements,
                dataManager.Stats.Stats,
                dataManager.Items.ItemDefinitionItemMiscRequirements,
                dataManager.Items.ItemMiscRequirements,
                itemApplicationManager.ItemRequirementsFactory);

            var normalItemGenerator = NormalItemGenerator.Create(
                itemApplicationManager.ItemFactory,
                itemApplicationManager.ItemMetaDataFactory,
                itemApplicationManager.ItemRequirementsFactory,
                dataManager.Items.ItemNamePartFactory,
                statRepository,
                dataManager.Items,
                itemStatGenerator,
                itemRequirementsGenerator);

            var itemTypeGeneratorClassName = typeof(MagicItemGenerator).FullName;
            var itemTypeGeneratorPlugin = dataManager.Items.ItemTypeGeneratorPlugins.GetByItemGeneratorClassName(itemTypeGeneratorClassName);

            var magicAffixGenerator = MagicAffixGenerator.Create(
                dataManager.Items.MagicTypesRandomAffixes,
                itemApplicationManager.ItemAffixGenerator);

            var magicItemNamer = MagicItemNamer.Create(dataManager.Items.ItemNamePartFactory);

            _magicItemGenerator = MagicItemGenerator.Create(
                itemTypeGeneratorPlugin.MagicTypeId,
                itemApplicationManager.ItemFactory,
                itemApplicationManager.ItemMetaDataFactory,
                normalItemGenerator,
                magicAffixGenerator,
                magicItemNamer);
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
