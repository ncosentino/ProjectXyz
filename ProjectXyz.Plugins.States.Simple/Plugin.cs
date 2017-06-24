using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.States;
using ProjectXyz.Api.States.Plugins;

namespace ProjectXyz.Plugins.States.Simple
{
    public sealed class Plugin : IStatePlugin
    {
        public IStateIdToTermRepository StateIdToTermRepository { get; }

        public IStateContextProvider StateContextProvider { get; }
    }
}
