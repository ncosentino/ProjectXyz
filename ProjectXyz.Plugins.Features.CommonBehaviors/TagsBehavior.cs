using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class TagsBehavior :
        BaseBehavior,
        ITagsBehavior
    {
        public TagsBehavior(params IIdentifier[] tags)
            : this((IEnumerable<IIdentifier>)tags)
        {
        }

        public TagsBehavior(IEnumerable<IIdentifier> tags)
        {
            Tags = tags.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> Tags { get; }
    }
}
