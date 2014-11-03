using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Application.Core.Items
{
    public class Item : IItem
    {
        #region Fields
        private readonly IMutableItemCollection _socketedItems;
        private readonly IEnchantmentBlock _enchantments;
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly ProjectXyz.Data.Interface.Items.IItem _item;

        private bool _enchantmentsDirty;
        private IDurability _durability;
        private double _weight;
        private double _value;
        private int _totalSockets;
        private int _openSockets;
        #endregion

        #region Constructors
        protected Item(IEnchantmentCalculator enchantmentCalculator, ProjectXyz.Data.Interface.Items.IItem item)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(item != null);

            _enchantmentCalculator = enchantmentCalculator;
            _item = item;

            _socketedItems = MutableItemCollection.Create();

            _enchantments = EnchantmentBlock.Create();
            foreach (var enchantment in _item.Enchantments)
            {
                _enchantments.Add(Enchantment.CreateFrom(enchantment));
            }

            _enchantmentsDirty = true;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _item.Id; }
        }

        public string Name
        {
            get { return _item.Name; }
        }

        public string MagicType
        {
            get { return _item.MagicType; }
        }

        public double Weight
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _weight;
            }
        }

        public double Value
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _value;
            }
        }

        public int RequiredSockets
        {
            get { return 1; }
        }

        public int TotalSockets
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _totalSockets;
            }
        }

        public int OpenSockets
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _openSockets;
            }
        }

        public IDurability Durability
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _durability;
            }
        }

        public IEnchantmentCollection Enchantments
        {
            get { return ReadonlyEnchantmentCollection.CreateCopy(_enchantments); }
        }

        public IRequirements Requirements
        {
            get { throw new NotImplementedException(); }
        }

        public IReadonlyItemCollection SocketedItems
        {
            get { return ReadonlyItemCollectionWrapper.Create(_socketedItems); }
        }
        #endregion

        #region Methods
        public static IItem Create(IEnchantmentCalculator enchantmentCalculator, ProjectXyz.Data.Interface.Items.IItem item)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(item != null);
            return new Item(enchantmentCalculator, item);
        }

        public void Enchant(IEnumerable<IEnchantment> enchantments)
        {
            _enchantments.AddRange(enchantments);
            ResetEnchantmentDependencyCache();
        }

        public void Disenchant(IEnumerable<IEnchantment> enchantments)
        {
            _enchantments.RemoveRange(enchantments);
            ResetEnchantmentDependencyCache();
        }

        public bool Socket(IItem item)
        {
            if (!item.CanBeUsedForSocketing() ||
                OpenSockets < item.RequiredSockets)
            {
                return false;
            }

            _socketedItems.Add(item);
            _openSockets -= item.RequiredSockets;
            return true;
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            var enchantmentCount = _enchantments.Count;
            _enchantments.UpdateElapsedTime(elapsedTime);
            _enchantmentsDirty |= enchantmentCount != _enchantments.Count;
        }

        protected void EnsureEnchantmentsCalculated()
        {
            if (!_enchantmentsDirty)
            {
                return;
            }

            var stats =_enchantmentCalculator.Calculate(_item.Stats, _enchantments);
            _durability = CalculateDurability(stats);
            _weight = CalculateWeight(stats);
            _value = CalculateValue(stats);
            _totalSockets = CalculateTotalSockets(stats);
            _openSockets = CalculateOpenSockets(_totalSockets, SocketedItems);

            _enchantmentsDirty = false;
        }

        private void ResetEnchantmentDependencyCache()
        {
            _enchantmentsDirty = true;
        }

        private IDurability CalculateDurability(IStatCollection<IStat> stats)
        {
            return ReadonlyDurability.Create(
                (int)stats[ItemStats.MaximumDurability].Value,
                (int)stats[ItemStats.CurrentDurability].Value);
        }

        private double CalculateWeight(IStatCollection<IStat> stats)
        {
            return stats[ItemStats.Weight].Value;
        }

        private double CalculateValue(IStatCollection<IStat> stats)
        {
            return stats[ItemStats.Value].Value;
        }

        private int CalculateTotalSockets(IStatCollection<IStat> stats)
        {
            return (int)stats[ItemStats.TotalSockets].Value;
        }

        private int CalculateOpenSockets(int totalSockets, IEnumerable<IItem> socketedItems)
        {
            return Math.Max(0, totalSockets - socketedItems.TotalRequiredSockets());
        }
        #endregion
    }
}
