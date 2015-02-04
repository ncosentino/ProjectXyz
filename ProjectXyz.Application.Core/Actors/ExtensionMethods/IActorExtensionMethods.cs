using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;

namespace ProjectXyz.Application.Core.Actors.ExtensionMethods
{
    public static class IActorExtensionMethods
    {
        #region Methods
        public static double GetMaximumLife(this IActor actor)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            return actor.Stats.GetValueOrDefault(ActorStats.MaximumLife, 0);
        }

        public static double GetCurrentLife(this IActor actor)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            return actor.Stats.GetValueOrDefault(ActorStats.CurrentLife, 0);
        }

        public static bool IsAlive(this IActor actor)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            return actor.GetCurrentLife() > 0;
        }

        public static bool IsDead(this IActor actor)
        {
            Contract.Requires<ArgumentNullException>(actor != null);
            return !actor.IsAlive();
        }
        #endregion
    }
}
