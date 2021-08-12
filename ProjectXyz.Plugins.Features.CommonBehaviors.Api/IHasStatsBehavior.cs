using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasStatsBehavior : IHasObservableStatsBehavior
    {
        Task MutateStatsAsync(Func<IDictionary<IIdentifier, double>, Task> callback);
    }
}