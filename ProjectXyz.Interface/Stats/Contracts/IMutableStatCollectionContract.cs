using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Interface.Stats.Contracts
{
    [ContractClassFor(typeof(IMutableStatCollection<>))]
    public abstract class IMutableStatCollectionContract<TStat> : IMutableStatCollection<TStat> where TStat : IStat
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

            set
            {
                Contract.Requires<ArgumentNullException>(statId != null);
                Contract.Requires<ArgumentException>(statId != string.Empty);
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value.Id == statId);
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
        #endregion
        
        public bool Contains(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TStat> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
