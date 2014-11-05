using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public abstract class StatCollection<TStat> : IStatCollection<TStat> where TStat : IStat
    {
        #region Fields
        private readonly Dictionary<string, TStat> _stats;
        #endregion

        #region Constructors
        protected StatCollection()
        {
            _stats = new Dictionary<string, TStat>();
        }

        protected StatCollection(IEnumerable<TStat> stats)
            : this()
        {
            Contract.Requires<ArgumentNullException>(stats != null);

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

        public TStat this[string id]
        {
            get
            {
                Contract.Assume(_stats[id] != null);
                return _stats[id];
            }
        }
        #endregion

        #region Methods
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

        protected void SetStat(TStat stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
            _stats[stat.Id] = stat;
        }

        protected void AddStat(TStat stat)
        {
            Contract.Requires<ArgumentNullException>(stat != null);
            _stats.Add(stat.Id, stat);
        }

        protected void RemoveStatWithId(string statId)
        {
            Contract.Requires<ArgumentNullException>(statId != null);
            Contract.Requires<ArgumentException>(statId != string.Empty);
            _stats.Remove(statId);
        }
        #endregion
    }
}
