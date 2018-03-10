using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Systems
{
    public interface ISystemUpdateComponentCreator
    {
        IComponent CreateNext();
    }
}
