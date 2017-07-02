using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Game.Interface.Systems
{
    public interface ISystemUpdateComponentCreator
    {
        IComponent CreateNext();
    }
}
