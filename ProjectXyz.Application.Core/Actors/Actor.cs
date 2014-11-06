using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;

using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Actors
{
    public class Actor : IActor
    {
        #region Fields
        private readonly IActorContext _context;
        private readonly ProjectXyz.Data.Interface.Actors.IActor _actor;
        #endregion

        #region Constructors
        protected Actor(IActorBuilder builder, IActorContext context, ProjectXyz.Data.Interface.Actors.IActor actor)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(actor != null);

            _context = context;
            _actor = actor;
        }
        #endregion

        #region Properties
        public double MaximumLife
        {
            get { throw new NotImplementedException(); }
        }

        public double CurrentLife
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region Methods
        public static IActor Create(IActorBuilder builder, IActorContext context, ProjectXyz.Data.Interface.Actors.IActor actor)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(actor != null);
            Contract.Ensures(Contract.Result<IActor>() != null);
            return new Actor(builder, context, actor);
        }

        public bool Equip(IItem item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
