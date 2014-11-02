using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach (var stat in stats)
            {
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
            get { return _stats[id]; }
        }

        protected IDictionary<string, TStat> Stats
        {
            get { return _stats; }
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
        #endregion
    }
}
