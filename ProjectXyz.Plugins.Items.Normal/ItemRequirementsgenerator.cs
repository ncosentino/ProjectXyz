using System;
using System.Linq;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Plugins.Items.Normal
{
    public sealed class ItemRequirementsGenerator : IItemRequirementsGenerator
    {
        #region Fields
        private readonly IItemDefinitionStatRequirementsRepository _itemDefinitionStatRequirementsRepository;
        private readonly IStatRepository _statRepository;
        private readonly IItemDefinitionItemMiscRequirementsRepository _itemDefinitionItemMiscRequirementsRepository;
        private readonly IItemMiscRequirementsRepository _itemMiscRequirementsRepository;
        private readonly IItemRequirementsFactory _itemRequirementsFactory;
        #endregion

        #region Constructors
        private ItemRequirementsGenerator(
            IItemDefinitionStatRequirementsRepository itemDefinitionStatRequirementsRepository,
            IStatRepository statRepository,
            IItemDefinitionItemMiscRequirementsRepository itemDefinitionItemMiscRequirementsRepository,
            IItemMiscRequirementsRepository itemMiscRequirementsRepository,
            IItemRequirementsFactory itemRequirementsFactory)
        {
            _itemDefinitionStatRequirementsRepository = itemDefinitionStatRequirementsRepository;
            _statRepository = statRepository;
            _itemDefinitionItemMiscRequirementsRepository = itemDefinitionItemMiscRequirementsRepository;
            _itemMiscRequirementsRepository = itemMiscRequirementsRepository;
            _itemRequirementsFactory = itemRequirementsFactory;
        }
        #endregion

        #region Methods
        public static IItemRequirementsGenerator Create(
            IItemDefinitionStatRequirementsRepository itemDefinitionStatRequirementsRepository,
            IStatRepository statRepository,
            IItemDefinitionItemMiscRequirementsRepository itemDefinitionItemMiscRequirementsRepository,
            IItemMiscRequirementsRepository itemMiscRequirementsRepository,
            IItemRequirementsFactory itemRequirementsFactory)
        {
            var generator = new ItemRequirementsGenerator(
                itemDefinitionStatRequirementsRepository,
                statRepository,
                itemDefinitionItemMiscRequirementsRepository,
                itemMiscRequirementsRepository,
                itemRequirementsFactory);
            return generator;
        }

        public IItemRequirements GenerateItemRequirements(Guid itemDefinitionId)
        {
            var requiredStats = _itemDefinitionStatRequirementsRepository
                .GetByItemDefinitionId(itemDefinitionId)
                .Select(x => _statRepository.GetById(x.StatId));
            var itemDefinitionItemMiscRequirements = _itemDefinitionItemMiscRequirementsRepository.GetByItemDefinitionId(itemDefinitionId);
            var itemMiscRequirements = _itemMiscRequirementsRepository.GetById(itemDefinitionItemMiscRequirements.ItemMiscRequirementsId);
            var itemRequirements = _itemRequirementsFactory.Create(
                itemMiscRequirements.RaceDefinitionId,
                itemMiscRequirements.ClassDefinitionId,
                requiredStats);
            return itemRequirements;
        }
        #endregion
    }
}