using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using Moq;

using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Core.Items;

namespace ProjectXyz.Tests.Data.Items.Mocks
{
    public class MockItemBuilder
    {
        #region Fields
        private readonly Mock<IItem> _item;
        private readonly List<IMutableStat> _stats;
        #endregion

        #region Constructors
        public MockItemBuilder()
        {
            _item = new Mock<IItem>();

            _stats = new List<IMutableStat>();
            _stats.Add(Stat.Create(ItemStats.CurrentDurability, 0));
            _stats.Add(Stat.Create(ItemStats.MaximumDurability, 0));
            _stats.Add(Stat.Create(ItemStats.Value, 0));
            _stats.Add(Stat.Create(ItemStats.Weight, 0));
            _stats.Add(Stat.Create(ItemStats.TotalSockets, 0));
        }
        #endregion

        #region Methods
        public MockItemBuilder WithStats(params IMutableStat[] stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);
            
            return WithStats((IEnumerable<IMutableStat>)stats);
        }

        public MockItemBuilder WithStats(IEnumerable<IMutableStat> stats)
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

            _item
                .Setup(x => x.EquippableSlots)
                .Returns(new List<string>(slots));
            return this;
        }
        
        public IItem Build()
        {
            Contract.Ensures(Contract.Result<IItem>() != null);

            _item
                .Setup(x => x.Stats)
                .Returns(StatCollection<IMutableStat>.Create(_stats));
            _item
                .Setup(x => x.Enchantments)
                .Returns(EnchantmentCollection.Create());
            _item
                .Setup(x => x.SocketedItems)
                .Returns(ItemCollection.Create());
            _item
                .Setup(x => x.Requirements)
                .Returns(new Mock<IRequirements>().Object);

            return _item.Object;
        }
        #endregion
    }
}
