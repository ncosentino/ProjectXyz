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
    public sealed class NamedItemAffixes : INamedItemAffixes
    {
        #region Fields
        private readonly INamedItemAffix _prefix;
        private readonly INamedItemAffix _suffix;
        #endregion

        #region Constructors
        private NamedItemAffixes(INamedItemAffix prefix, INamedItemAffix suffix)
        {
            Contract.Requires<ArgumentNullException>(prefix != null || suffix != null);

            _prefix = prefix;
            _suffix = suffix;
        }
        #endregion

        #region Properties
        public INamedItemAffix Prefix
        {
            get { return _prefix; }
        }

        public INamedItemAffix Suffix
        {
            get { return _suffix; }
        }
        #endregion

        #region Methods
        public static INamedItemAffixes Create(INamedItemAffix prefix, INamedItemAffix suffix)
        {
            Contract.Requires<ArgumentNullException>(prefix != null || suffix != null);
            Contract.Ensures(Contract.Result<INamedItemAffixes>() != null);

            return new NamedItemAffixes(prefix, suffix);
        }
        #endregion
    }
}
