using ProjectXyz.Application.Interface.Triggering.Triggers.Elapsed;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Triggering.Triggers.Elapsed
{
    public sealed class ElapsedTimeTriggerComponent : IElapsedTimeTriggerComponent
    {
        public ElapsedTimeTriggerComponent(IInterval elapsed)
        {
            Elapsed = elapsed;
        }

        public IInterval Elapsed { get; }
    }
}