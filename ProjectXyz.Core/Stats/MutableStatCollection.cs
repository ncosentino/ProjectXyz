using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Stats;

namespace ProjectXyz.Core.Stats
{
    public class MutableStatCollection<TStat> : StatCollection<TStat>, IMutableStatCollection<TStat> where TStat : IStat
    {
        #region Constructors
        protected MutableStatCollection()
            : this(new TStat[0])
        {
        }

        protected MutableStatCollection(IEnumerable<TStat> stats)
            : base(stats)
        {
        }
        #endregion

        #region Properties
        new public TStat this[string id]
        {
            get { return Stats[id]; }
            set { Stats[id] = value; }
        }
        #endregion

        #region Methods
        public static IMutableStatCollection<TStat> Create()
        {
            return new MutableStatCollection<TStat>();
        }

        public static IMutableStatCollection<TStat> Create(IEnumerable<TStat> stats)
        {
            return new MutableStatCollection<TStat>(stats);
        }

        public void Set(TStat stat)
        {
            this.Stats[stat.Id] = stat;
        }

        public void Add(TStat stat)
        {
            this.Stats.Add(stat.Id, stat);
        }

        public void AddRange(IEnumerable<TStat> stats)
        {
            foreach (var stat in stats)
            {
                this.Stats.Add(stat.Id, stat);
            }
        }

        public void Remove(string id)
        {
            this.Stats.Remove(id);
        }

        public void Remove(TStat stat)
        {
            this.Stats.Remove(stat.Id);
        }

        public void RemoveRange(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                this.Stats.Remove(id);
            }
        }

        public void RemoveRange(IEnumerable<TStat> stats)
        {
            foreach (var stat in stats)
            {
                this.Stats.Remove(stat.Id);
            }
        }

        public new System.Collections.IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
        #endregion
    }
}
