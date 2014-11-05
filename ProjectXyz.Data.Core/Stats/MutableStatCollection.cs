using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
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
            Contract.Requires<ArgumentNullException>(stats != null);
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
            SetStat(stat);
        }

        public void Add(TStat stat)
        {
            AddStat(stat);
        }

        public void AddRange(IEnumerable<TStat> stats)
        {
            foreach (var stat in stats)
            {
                if (stat == null)
                {
                    throw new NullReferenceException("Stats within the provided enumerable cannot be null.");
                }

                AddStat(stat);
            }
        }

        public void Remove(string id)
        {
            RemoveStatWithId(id);
        }

        public void Remove(TStat stat)
        {
            RemoveStatWithId(stat.Id);
        }

        public void RemoveRange(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                RemoveStatWithId(id);
            }
        }

        public void RemoveRange(IEnumerable<TStat> stats)
        {
            foreach (var stat in stats)
            {
                RemoveStatWithId(stat.Id);
            }
        }

        public new System.Collections.IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
        #endregion
    }
}
