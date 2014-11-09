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
        private readonly IMutableStatCollection _stats;
        private readonly IMutableEnchantmentCollection _enchantments;
        private readonly IRequirements _requirements;
        private readonly IMutableItemCollection _socketedItems;
        private readonly List<string> _equippableSlots;
        
        private Guid _id;
        #endregion

        #region Constructors
        protected Item()
        {
            _id = Guid.NewGuid();
            
            _stats = StatCollection.Create();
            _enchantments = EnchantmentCollection.Create();
            _requirements = Items.Requirements.Create();
            _socketedItems = ItemCollection.Create();
            _equippableSlots = new List<string>();

            this.MaterialType =
            this.Name =
            this.ItemType =
            this.MagicType = "Default";
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

        public IMutableStatCollection Stats
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

        public IList<string> EquippableSlots
        {
            get { return _equippableSlots; }
        }

        public Guid Id
        {
            get { return _id; }

            set { _id = value; }
        }
        #endregion

        #region Methods
        public static IItem Create()
        {
            Contract.Ensures(Contract.Result<IItem>() != null);
            return new Item();
        }
        #endregion
    }
}
