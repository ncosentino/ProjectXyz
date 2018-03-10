namespace ProjectXyz.Api.Framework.Entities
{
    public interface IEntity
    {
        IComponentCollection Components { get; }
    }
}