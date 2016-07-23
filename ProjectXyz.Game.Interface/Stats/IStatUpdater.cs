using ProjectXyz.Application.Interface.Triggering.Triggers.Elapsed;

namespace ProjectXyz.Game.Interface.Stats
{
    public interface IStatUpdater
    {
        void Update(
            IElapsedTimeTriggerMechanic elapsedTimeTriggerMechanic,
            IElapsedTimeTriggerComponent elapsedTimeTriggerComponent);
    }
}