using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Interface.Stats.Contracts
{
    [ContractClassFor(typeof(IStatCollection<>))]
    public abstract class IStatCollectionContract<TStat> : IStatCollection<TStat> where TStat : IStat
    {
        #region Properties
        public abstract int Count { get; }

        public TStat this[string statId]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(statId != null);
                Contract.Requires<ArgumentException>(statId != string.Empty);
                return default(TStat);
            }
        }
        #endregion

        #region Methods
        public bool Contains(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);
            Contract.Requires<ArgumentException>(id != string.Empty);
            return default(bool);
        }

        public IEnumerator<TStat> GetEnumerator()
        {
            return default(IEnumerator<TStat>);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return default(System.Collections.IEnumerator);
        }
        #endregion
    }
}
