using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Items;
using ProjectXyz.Interface.Enchantments;
using ProjectXyz.Interface.Stats;
using ProjectXyz.Core.Items;
using ProjectXyz.Core.Enchantments;
using ProjectXyz.Core.Stats;

namespace ProjectXyz.Core.Items
{
    public class Item : IItem
    {
        #region Fields
        private readonly IMutableStatCollection<IMutableStat> _stats;
        private readonly IEnchantmentBlock _enchantments;
        private readonly IEnchantmentCalculator _enchantmentCalculator;

        private bool _enchantmentsDirty;
        private IDurability _durability;
        private double _weight;
        private double _value;
        #endregion

        #region Constructors
        protected Item(IEnchantmentCalculator enchantmentCalculator)
        {
            _enchantmentCalculator = enchantmentCalculator;

            _enchantments = EnchantmentBlock.Create();
            _stats = MutableStatCollection<IMutableStat>.Create();
            _stats.Add(MutableStat.Create(ItemStats.MaximumDurability));
            _stats.Add(MutableStat.Create(ItemStats.CurrentDurability));
            _stats.Add(MutableStat.Create(ItemStats.Weight));
            _stats.Add(MutableStat.Create(ItemStats.Value));

            _enchantmentsDirty = true;
            this.Id = Guid.NewGuid();
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string MagicType
        {
            get;
            private set;
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
            get { return _enchantments; }
        }

        public IRequirements Requirements
        {
            get { throw new NotImplementedException(); }
        }

        public IInventory Sockets
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region Methods
        public static IItem Create(IEnchantmentCalculator enchantmentCalculator)
        {
            return new Item(enchantmentCalculator);
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

            var stats =_enchantmentCalculator.Calculate(_stats, _enchantments);
            _durability = CalculateDurability(stats);
            _weight = CalculateWeight(stats);
            _value = CalculateValue(stats);

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
        #endregion
    }
}
