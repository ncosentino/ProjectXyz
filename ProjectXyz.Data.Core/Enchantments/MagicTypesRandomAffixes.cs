using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public sealed class MagicTypesRandomAffixes : IMagicTypesRandomAffixes
    {
        #region Constructors
        private MagicTypesRandomAffixes(Guid id, Guid magicTypeId, int minimumAffixes, int maximumAffixes)
        {
            Contract.Requires<ArgumentOutOfRangeException>(minimumAffixes >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(minimumAffixes <= maximumAffixes);

            this.Id = id;
            this.MagicTypeId = magicTypeId;
            this.MinimumAffixes = minimumAffixes;
            this.MaximumAffixes = maximumAffixes;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            private set;
        }

        public Guid MagicTypeId
        {
            get;
            private set;
        }

        public int MinimumAffixes
        {
            get;
            private set;
        }

        public int MaximumAffixes
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IMagicTypesRandomAffixes Create(Guid id, Guid magicTypeId, int minimumAffixes, int maximumAffixes)
        {
            Contract.Requires<ArgumentOutOfRangeException>(minimumAffixes >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(minimumAffixes <= maximumAffixes);
            Contract.Ensures(Contract.Result<IMagicTypesRandomAffixes>() != null);

            return new MagicTypesRandomAffixes(id, magicTypeId, minimumAffixes, maximumAffixes);
        }
        #endregion
    }
}
