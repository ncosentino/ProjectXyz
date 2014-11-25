using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Stats.Contracts
{
    [ContractClassFor(typeof(IStatCollection))]
    public abstract class IStatCollectionContract : IStatCollection
    {
        #region Properties
        public abstract int Count { get; }

        public IStat this[Guid statId]
        {
            get
            {
                Contract.Ensures(Contract.Result<IStat>() != null);
                return default(IStat);
            }
        }
        #endregion

        #region Methods
        public bool Contains(Guid id)
        {
            return default(bool);
        }

        public IEnumerator<IStat> GetEnumerator()
        {
            return default(IEnumerator<IStat>);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return default(System.Collections.IEnumerator);
        }
        #endregion
    }
}
