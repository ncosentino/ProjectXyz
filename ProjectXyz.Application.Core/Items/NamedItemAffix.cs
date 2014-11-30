using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public class NamedItemAffix : INamedItemAffix
    {
        #region Fields
        private readonly string _name;
        private readonly List<IEnchantment> _enchantments;
        #endregion

        #region Constructors
        private NamedItemAffix(string name, IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name.Trim().Length >= 0);
            Contract.Requires<ArgumentNullException>(enchantments != null);

            _name = name;
            _enchantments = new List<IEnchantment>(enchantments);
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<IEnchantment> Enchantments
        {
            get { return _enchantments; }
        }
        #endregion

        #region Methods
        public static INamedItemAffix Create(string name, IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name.Trim().Length >= 0);
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<INamedItemAffix>() != null);

            return new NamedItemAffix(name, enchantments);
        }
        #endregion
    }
}
