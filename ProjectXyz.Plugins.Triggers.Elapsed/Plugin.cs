using System.Collections.Generic;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Api.Triggering.Plugins;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Triggers.Elapsed.Duration;

namespace ProjectXyz.Plugins.Triggers.Elapsed
{
    public sealed class Plugin : ITriggerPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            SharedComponents = new ComponentCollection(new IComponent[]
            {
                new GenericComponent<IDurationTriggerMechanicFactory>(new DurationTriggerMechanicFactory()),
                new GenericComponent<IElapsedTimeTriggerSourceMechanicRegistrar>(new ElapsedTimeTriggerSourceMechanicRegistrar()), 
            });
        }

        public IReadOnlyCollection<ITriggerSourceMechanic> TriggerSourceMechanics { get; }

        public IReadOnlyCollection<ITriggerMechanicRegistrar> TriggerMechanicRegistrars { get; }

        public IComponentCollection SharedComponents { get; }
    }
}
