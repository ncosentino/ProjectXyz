using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Triggering;
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

            TriggerSourceMechanics = new[]
            {
                elapsedTimeTriggerSourceMechanic
            };
            TriggerMechanicRegistrars = new[]
            {
                elapsedTimeTriggerSourceMechanic
            };

            SharedComponents = new ComponentCollection(new[]
            {
                new DurationTriggerMechanicFactory(),
            });
        }

        public IReadOnlyCollection<ITriggerSourceMechanic> TriggerSourceMechanics { get; }

        public IReadOnlyCollection<ITriggerMechanicRegistrar> TriggerMechanicRegistrars { get; }

        public IComponentCollection SharedComponents { get; }
    }
}
