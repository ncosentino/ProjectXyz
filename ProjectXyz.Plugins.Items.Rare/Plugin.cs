using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql.Stats;
using ProjectXyz.Plugins.Items.Normal;

namespace ProjectXyz.Plugins.Items.Rare
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

            var normalItemGenerator = NormalItemGenerator.Create(
                itemApplicationManager.ItemFactory,
                itemApplicationManager.ItemMetaDataFactory,
                itemApplicationManager.ItemRequirementsFactory,
                dataManager.Items.ItemNamePartFactory,
                statRepository,
                dataManager.Items,
                statApplicationManager.StatGenerator,
                itemRequirementsGenerator);

            var itemTypeGeneratorClassName = typeof(RareItemGenerator).FullName;
            var itemTypeGeneratorPlugin = dataManager.Items.ItemTypeGeneratorPlugins.GetByItemGeneratorClassName(itemTypeGeneratorClassName);
            
            var itemNamer = RareItemNamer.Create(
                dataManager.Items.ItemNamePartFactory,
                dataManager.Items.ItemNameAffixFilter);

            _magicItemGenerator = RareItemGenerator.Create(
                itemTypeGeneratorPlugin.MagicTypeId,
                itemApplicationManager.ItemFactory,
                itemApplicationManager.ItemMetaDataFactory,
                normalItemGenerator,
                itemApplicationManager.ItemAffixGenerator,
                itemNamer);
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
