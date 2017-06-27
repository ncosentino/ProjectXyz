using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IHasBehaviors
    {
        IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
