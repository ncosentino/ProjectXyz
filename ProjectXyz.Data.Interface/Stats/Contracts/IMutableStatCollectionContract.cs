using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Stats.Contracts
{
    [ContractClassFor(typeof(IMutableStatCollection<>))]
    public abstract class IMutableStatCollectionContract<TStat> : IMutableStatCollection<TStat> where TStat : IStat
    {
        #region Properties
        public abstract int Count { get; }

        public abstract TStat this[string statId] { get; }

        TStat IMutableStatCollection<TStat>.this[string id]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(id != null);
                Contract.Requires<ArgumentNullException>(id != string.Empty);
                Contract.Ensures(Contract.Result<TStat>() != null);
                return default(TStat);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
            }
        }
        #endregion

        #region Methods
        public void Set(TStat stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
        }

        public void Add(TStat stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
        }

        public void AddRange(IEnumerable<TStat> stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
        }

        public void Remove(string statId)
        {
            Contract.Requires<ArgumentNullException>(statId != null);
        }

        public void Remove(TStat stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
        }

        public void RemoveRange(IEnumerable<TStat> stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
        }

        public void RemoveRange(IEnumerable<string> statIds)
        {
            Contract.Requires<ArgumentNullException>(statIds != null);
        }
        
        public abstract bool Contains(string id);

        public abstract IEnumerator<TStat> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return default(System.Collections.IEnumerator);
        }
        #endregion
    }
}
