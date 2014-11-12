using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Stats.Contracts
{
    [ContractClassFor(typeof(IMutableStatCollection))]
    public abstract class IMutableStatCollectionContract : IMutableStatCollection
    {
        #region Properties
        public abstract int Count { get; }

        public abstract IStat this[string statId] { get; }

        IStat IMutableStatCollection.this[string id]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(id != null);
                Contract.Requires<ArgumentNullException>(id != string.Empty);
                Contract.Ensures(Contract.Result<IStat>() != null);
                return default(IStat);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
            }
        }
        #endregion

        #region Methods
        public void Set(IStat stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
        }
        
        public void Add(IEnumerable<IStat> stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
        }

        public bool Remove(IEnumerable<IStat> stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
            return default(bool);
        }

        public bool Remove(IEnumerable<string> statIds)
        {
            Contract.Requires<ArgumentNullException>(statIds != null);
            return default(bool);
        }
        
        public abstract bool Contains(string id);

        public abstract IEnumerator<IStat> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return default(System.Collections.IEnumerator);
        }
        #endregion
    }
}
