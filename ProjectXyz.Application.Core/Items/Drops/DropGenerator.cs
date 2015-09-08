using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Drops;
using ProjectXyz.Data.Interface.Items.Drops;
using ProjectXyz.Data.Interface.Items.MagicTypes;

namespace ProjectXyz.Application.Core.Items.Drops
{
    public sealed class DropGenerator : IDropGenerator
    {
        #region Fields
        private readonly IDropRepository _dropRepository;
        private readonly IDropEntryRepository _dropEntryRepository;
        private readonly IDropLinkRepository _dropLinkRepository;
        private readonly IItemGenerator _itemGenerator;
        private readonly IMagicTypeGroupingRepository _magicTypeGroupingRepository;
        private readonly IMagicTypeRepository _magicTypeRepository;
        #endregion

        #region Constructors
        private DropGenerator(
            IDropRepository dropRepository,
            IDropEntryRepository dropEntryRepository,
            IDropLinkRepository dropLinkRepository,
            IMagicTypeRepository magicTypeRepository,
            IMagicTypeGroupingRepository magicTypeGroupingRepository,
            IItemGenerator itemGenerator)
        {
            _dropRepository = dropRepository;
            _dropEntryRepository = dropEntryRepository;
            _dropLinkRepository = dropLinkRepository;
            _magicTypeRepository = magicTypeRepository;
            _magicTypeGroupingRepository = magicTypeGroupingRepository;
            _itemGenerator = itemGenerator;
        }
        #endregion

        #region Methods
        public static IDropGenerator Create(
            IDropRepository dropRepository,
            IDropEntryRepository dropEntryRepository,
            IDropLinkRepository dropLinkRepository,
            IMagicTypeRepository magicTypeRepository,
            IMagicTypeGroupingRepository magicTypeGroupingRepository,
            IItemGenerator itemGenerator)
        {
            var generator = new DropGenerator(
                dropRepository,
                dropEntryRepository,
                dropLinkRepository,
                magicTypeRepository,
                magicTypeGroupingRepository,
                itemGenerator);
            return generator;
        }

        public IEnumerable<IItem> Generate(
            IRandom randomizer,
            Guid dropId,
            int level,
            IItemContext itemContext)
        {
            var parentDrop = _dropRepository.GetById(dropId);
            var dropQueue = new Queue<IDrop>();
            dropQueue.Enqueue(parentDrop);

            while (dropQueue.Count > 0)
            {
                var currentDrop = dropQueue.Dequeue();

                var targetCount =
                    currentDrop.Minimum +
                    (int)Math.Round(randomizer.NextDouble() * (currentDrop.Maximum - currentDrop.Minimum));

                var orderedDropWeightings = GetOrderedDropWeightings(
                    _dropEntryRepository,
                    _dropLinkRepository,
                    currentDrop.Id)
                    .ToArray();

                // ensure that if we cannot repeat entries that we have 
                // sufficient entries to meet our target count of drops
                if (!currentDrop.CanRepeat && orderedDropWeightings.Length < targetCount)
                {
                    throw new InvalidOperationException(string.Format(
                        "The current drop '{0}' is set to not repeat entries, but there are insufficient entries ({1}) to meet the rolled target number of entries ({2}).", 
                        currentDrop.Id, 
                        orderedDropWeightings.Length, 
                        targetCount));
                }
                
                var orderedDropMapping = CreateOrderedDropMapping(orderedDropWeightings);

                for (int i = 0; i < targetCount; ++i)
                {
                    var roll = randomizer.NextDouble();
                    foreach (var entry in orderedDropMapping)
                    {
                        if (roll > entry.Key)
                        {
                            continue;
                        }

                        if (entry.Value is IDropEntry)
                        {
                            var dropEntry = (IDropEntry)entry.Value;

                            var rolledMagicType = RollForMagicType(
                                randomizer,
                                _magicTypeRepository,
                                _magicTypeGroupingRepository,
                                dropEntry.MagicTypeGroupingId);
                            var magicTypeId = rolledMagicType.Id;

                            var droppedItem = _itemGenerator.Generate(
                                randomizer,
                                dropEntry.ItemDefinitionId,
                                magicTypeId,
                                level,
                                itemContext);
                            yield return droppedItem;
                        }
                        else if (entry.Value is IDropLink)
                        {
                            var childDropId = ((IDropLink)entry.Value).ChildDropId;
                            var childDrop = _dropRepository.GetById(childDropId);
                            dropQueue.Enqueue(childDrop);
                        }
                        else
                        {
                            throw new NotSupportedException(string.Format("Drop of type '{0}' is not supported.", entry.Value.GetType()));
                        }

                        break;
                    }

                    // if we cannot repeat entries, we need to re-adjust for the remaining entries
                    if (!currentDrop.CanRepeat && i < targetCount - 1)
                    {
                        orderedDropWeightings = GetOrderedDropWeightings(
                            _dropEntryRepository,
                            _dropLinkRepository,
                            currentDrop.Id)
                            .ToArray();
                        orderedDropMapping = CreateOrderedDropMapping(orderedDropWeightings);
                    }
                }
            }
        }

        private IMagicType RollForMagicType(
            IRandom randomizer,
            IMagicTypeRepository magicTypeRepository,
            IMagicTypeGroupingRepository magicTypeGroupingRepository,
            Guid magicTypeGroupingId)
        {
            var magicTypeGroupings = magicTypeGroupingRepository
                .GetByGroupingId(magicTypeGroupingId)
                .ToArray();
            var possibleMagicTypes = magicTypeRepository
              .GetAll()
              .Where(x => magicTypeGroupings.FirstOrDefault(y => y.MagicTypeId == x.Id) != null)
              .ToArray();
            
            var orderedMagicTypeMapping = CreateOrderedMagicTypeMapping(possibleMagicTypes);

            var roll = randomizer.NextDouble();
            foreach (var entry in orderedMagicTypeMapping)
            {
                if (roll > entry.Key)
                {
                    continue;
                }

                return entry.Value;
            }

            throw new InvalidOperationException(string.Format("No magic type was rolled for magic type grouping '{0}'.", magicTypeGroupingId));
        }

        private IDictionary<double, IMagicType> CreateOrderedMagicTypeMapping(IMagicType[] possibleMagicTypes)
        {
            var totalWeighting = possibleMagicTypes.Sum(x => x.RarityWeighting);
            var runningSumWeighting = 0d;

            var orderedMagicTypeMapping = new SortedDictionary<double, IMagicType>();
            foreach (var entry in possibleMagicTypes)
            {
                var normalizedWeighting = entry.RarityWeighting / (double)totalWeighting;
                runningSumWeighting += normalizedWeighting;

                orderedMagicTypeMapping[runningSumWeighting] = entry;
            }

            return orderedMagicTypeMapping;
        }

        private IDictionary<double, IHasDropWeighting> CreateOrderedDropMapping(IHasDropWeighting[] orderedDropWeightings)
        {
            var totalWeighting = orderedDropWeightings.Sum(x => x.Weighting);
            var runningSumWeighting = 0d;

            var orderedDropMapping = new SortedDictionary<double, IHasDropWeighting>();
            foreach (var entry in orderedDropWeightings)
            {
                var normalizedWeighting = entry.Weighting / (double)totalWeighting;
                runningSumWeighting += normalizedWeighting;

                orderedDropMapping[runningSumWeighting] = entry;
            }

            return orderedDropMapping;
        }

        private IEnumerable<IHasDropWeighting> GetOrderedDropWeightings(
            IDropEntryRepository dropEntryRepository,
            IDropLinkRepository dropLinkRepository,
            Guid dropId)
        {
            var drops = dropEntryRepository
                .GetByParentDropId(dropId);
            var dropLinks = dropLinkRepository
                .GetByParentDropId(dropId);
            var orderedDropWeightings = drops
                .Cast<IHasDropWeighting>()
                .Concat(dropLinks.Cast<IHasDropWeighting>())
                .OrderBy(x => x.Weighting);
            return orderedDropWeightings;
        }
        #endregion
    }
}
