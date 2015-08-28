using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IItemStore))]
    public abstract class IItemStoreContract : IItemStore
    {
        #region Properties
        public Guid NameStringResourceId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid InventoryGraphicResourceId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid MagicTypeId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid ItemTypeId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid MaterialTypeId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid SocketTypeId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }
        
        public abstract Guid Id { get; }
        #endregion
    }
}
