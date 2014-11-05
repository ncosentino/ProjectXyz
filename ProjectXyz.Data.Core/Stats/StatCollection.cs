using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public class MutableStatCollection<TStat> : IMutableStatCollection<TStat> where TStat : IStat
    {
        #region Fields
        private readonly Dictionary<string, TStat> _stats;
        #endregion

        #region Constructors
        protected MutableStatCollection()
            : this(new TStat[0])
        {
        }

        protected MutableStatCollection(IEnumerable<TStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);

            _stats = new Dictionary<string, TStat>();
            foreach (var stat in stats)
            {
                if (stat == null)
                {
                    throw new NullReferenceException("Stats within the provided enumerable cannot be null.");
                }

                _stats[stat.Id] = stat;
            }
        }
        #endregion

        #region Properties
        public int Count
        {
            get { return _stats.Count; }
        }

        TStat IStatCollection<TStat>.this[string id]
        {
            get
            {
                Contract.Assume(_stats[id] != null);
                return _stats[id];
            }
        }

        TStat IMutableStatCollection<TStat>.this[string id]
        {
            get 
            {
                Contract.Assume(_stats[id] != null); 
                return _stats[id]; 
            }

            set 
            {
                _stats[id] = value; 
            }
        }
        #endregion

        #region Methods
        public static IMutableStatCollection<TStat> Create()
        {
            Contract.Ensures(Contract.Result<IMutableStatCollection<TStat>>() != null);
            return new MutableStatCollection<TStat>();
        }

        public static IMutableStatCollection<TStat> Create(IEnumerable<TStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<IMutableStatCollection<TStat>>() != null);
            return new MutableStatCollection<TStat>(stats);
        }

        public void Set(TStat stat)
        {
            _stats[stat.Id] = stat;
        }

        public void Add(TStat stat)
        {
            _stats.Add(stat.Id, stat);
        }

        public void AddRange(IEnumerable<TStat> stats)
        {
            foreach (var stat in stats)
            {
                if (stat == null)
                {
                    throw new NullReferenceException("Stats within the provided enumerable cannot be null.");
                }

                Add(stat);
            }
        }

        public void Remove(string id)
        {
            _stats.Remove(id);
        }

        public void Remove(TStat stat)
        {
            Remove(stat.Id);
        }

        public void RemoveRange(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                Remove(id);
            }
        }

        public void RemoveRange(IEnumerable<TStat> stats)
        {
            foreach (var stat in stats)
            {
                Remove(stat.Id);
            }
        }

        public bool Contains(string id)
        {
            return _stats.ContainsKey(id);
        }

        public IEnumerator<TStat> GetEnumerator()
        {
            return _stats.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _stats.Values.GetEnumerator();
        }
        #endregion        
    }
}
