using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Game.Interface.Stats
{
    public interface IStatUpdater
    {
        void Update(IInterval elapsed);
    }
}