using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
            var elapsedTimeTriggerSourceMechanic = new ElapsedTimeTriggerSourceMechanic();

            SharedComponents = new ComponentCollection(new IComponent[]
            {
                new GenericComponent<IDurationTriggerMechanicFactory>(new DurationTriggerMechanicFactory()),
                new GenericComponent<ITriggerSourceMechanic>(elapsedTimeTriggerSourceMechanic), 
                new GenericComponent<ITriggerMechanicRegistrar>(elapsedTimeTriggerSourceMechanic), 
            });
        }

        public IReadOnlyCollection<ITriggerSourceMechanic> TriggerSourceMechanics { get; }

        public IReadOnlyCollection<ITriggerMechanicRegistrar> TriggerMechanicRegistrars { get; }

        public IComponentCollection SharedComponents { get; }
    }
}
