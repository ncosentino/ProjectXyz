using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Api.Triggering.Plugins
{
    public interface ITriggerPlugin : IPlugin
    {
        IReadOnlyCollection<ITriggerSourceMechanic> TriggerSourceMechanics { get; }

        IReadOnlyCollection<ITriggerMechanicRegistrar> TriggerMechanicRegistrars { get; }

        IComponentCollection SharedComponents { get; }
    }
}