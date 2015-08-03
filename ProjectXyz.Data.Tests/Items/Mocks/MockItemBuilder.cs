using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Items;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Tests.Items.Mocks
{
    public class MockItemBuilder
    {
        #region Fields
        private readonly Mock<IItemStore> _item;
        private readonly List<IStat> _stats;
        private readonly List<string> _equippableSlots;

        private Guid _socketTypeId;
        #endregion

        #region Constructors
        public MockItemBuilder()
        {
            _item = new Mock<IItemStore>();
            _stats = new List<IStat>();
            _equippableSlots = new List<string>();
            _socketTypeId = Guid.NewGuid();
        }
        #endregion

        #region Methods
        public MockItemBuilder WithStats(params IStat[] stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            return WithStats((IEnumerable<IStat>)stats);
        }

        public MockItemBuilder WithStats(IEnumerable<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            _stats.AddRange(stats);
            return this;
        }

        public MockItemBuilder WithEquippableSlots(params string[] slots)
        {
            Contract.Requires<ArgumentNullException>(slots != null);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            return WithEquippableSlots((IEnumerable<string>)slots);
        }

        public MockItemBuilder WithEquippableSlots(IEnumerable<string> slots)
        {
            Contract.Requires<ArgumentNullException>(slots != null);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            _equippableSlots.AddRange(slots);
            return this;
        }

        public MockItemBuilder WithSocketTypeId(Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            _socketTypeId = socketTypeId;
            return this;
        }
        
        public IItemStore Build()
        {
            Contract.Ensures(Contract.Result<IItemStore>() != null);

            _item
                .Setup(x => x.Stats)
                .Returns(StatCollection.Create(_stats));
            _item
                .Setup(x => x.Enchantments)
                .Returns(EnchantmentCollection.Create());
            _item
                .Setup(x => x.SocketedItems)
                .Returns(ItemStoreCollection.Create());
            _item
                .Setup(x => x.Requirements)
                .Returns(new MockRequirementsBuilder().Build());
            _item
                .Setup(x => x.Id)
                .Returns(Guid.NewGuid());
            _item
                .Setup(x => x.Name)
                .Returns("Default");
            _item
                .Setup(x => x.InventoryGraphicResource)
                .Returns("Default");
            _item
                .Setup(x => x.MagicTypeId)
                .Returns(Guid.Empty);
            _item
                .Setup(x => x.MaterialTypeId)
                .Returns(Guid.NewGuid());
            _item
                .Setup(x => x.SocketTypeId)
                .Returns(_socketTypeId);
            _item
                .Setup(x => x.ItemType)
                .Returns("Default");
            _item
                .Setup(x => x.EquippableSlots)
                .Returns(new List<string>(_equippableSlots));

            return _item.Object;
        }
        #endregion
    }
}
