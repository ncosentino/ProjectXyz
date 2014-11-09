using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public class StatCollection : IMutableStatCollection
    {
        #region Fields
        private readonly Dictionary<string, IStat> _stats;
        #endregion

        #region Constructors
        protected StatCollection()
            : this(new IStat[0])
        {
        }

        protected StatCollection(IEnumerable<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);

            _stats = new Dictionary<string, IStat>();
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

        IStat IStatCollection.this[string id]
        {
            get
            {
                Contract.Assume(_stats[id] != null);
                return _stats[id];
            }
        }

        IStat IMutableStatCollection.this[string id]
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
        public static IMutableStatCollection Create()
        {
            Contract.Ensures(Contract.Result<IMutableStatCollection>() != null);
            return new StatCollection();
        }

        public static IMutableStatCollection Create(IEnumerable<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<IMutableStatCollection>() != null);
            return new StatCollection(stats);
        }

        public void Set(IStat stat)
        {
            _stats[stat.Id] = stat;
        }

        public void Add(IStat stat)
        {
            _stats.Add(stat.Id, stat);
        }

        public void AddRange(IEnumerable<IStat> stats)
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

        public void Remove(IStat stat)
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

        public void RemoveRange(IEnumerable<IStat> stats)
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

        public IEnumerator<IStat> GetEnumerator()
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
