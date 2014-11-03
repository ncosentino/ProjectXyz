using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Core.Enchantments;

namespace ProjectXyz.Data.Core.Items
{
    public class Item : IItem
    {
        #region Fields
        private readonly IMutableStatCollection<IMutableStat> _stats;
        private readonly IMutableEnchantmentCollection _enchantments;
        private readonly IRequirements _requirements;
        private readonly IMutableItemCollection _socketedItems;
        private readonly Guid _id;
        #endregion

        #region Constructors
        protected Item()
        {
            _id = Guid.NewGuid();
            
            _stats = MutableStatCollection<IMutableStat>.Create();
            _stats.Set(MutableStat.Create(ItemStats.CurrentDurability, 0));
            _stats.Set(MutableStat.Create(ItemStats.MaximumDurability, 0));
            _stats.Set(MutableStat.Create(ItemStats.Value, 0));
            _stats.Set(MutableStat.Create(ItemStats.Weight, 0));
            _stats.Set(MutableStat.Create(ItemStats.TotalSockets, 0));

            _enchantments = MutableEnchantmentCollection.Create();
            _requirements = null;
            _socketedItems = MutableItemCollection.Create();

            this.MaterialType =
            this.Name =
            this.ItemType =
            this.MagicType = "Default;";
        }
        #endregion

        #region Properties
        public string Name
        {
            get;
            set;
        }

        public string MagicType
        {
            get;
            set;
        }

        public string ItemType
        {
            get;
            set;
        }

        public string MaterialType
        {
            get;
            set;
        }

        public IMutableStatCollection<IMutableStat> Stats
        {
            get { return _stats; }
        }

        public IMutableEnchantmentCollection Enchantments
        {
            get { return _enchantments; }
        }

        public IRequirements Requirements
        {
            get { return _requirements; }
        }

        public IMutableItemCollection SocketedItems
        {
            get { return _socketedItems; }
        }

        public Guid Id
        {
            get { return _id; }
        }
        #endregion

        #region Methods
        public static IItem Create()
        {
            return new Item();
        }
        #endregion
    }
}
