using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public class NamedItemAffix : INamedItemAffix
    {
        #region Fields
        private readonly Guid _nameStringResourceId;
        private readonly List<IEnchantment> _enchantments;
        #endregion

        #region Constructors
        private NamedItemAffix(
            Guid nameStringResourceId,
            IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(enchantments != null);

            _nameStringResourceId = nameStringResourceId;
            _enchantments = new List<IEnchantment>(enchantments);
        }
        #endregion

        #region Properties
        public Guid NameStringResourceId
        {
            get { return _nameStringResourceId; }
        }

        public IEnumerable<IEnchantment> Enchantments
        {
            get { return _enchantments; }
        }
        #endregion

        #region Methods
        public static INamedItemAffix Create(
            Guid nameStringResourceId,
            IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<INamedItemAffix>() != null);

            return new NamedItemAffix(
                nameStringResourceId,
                enchantments);
        }
        #endregion
    }
}
