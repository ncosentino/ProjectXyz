namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface ISerializableConverter
    {
        TSerializable Convert<TSerializable>(ISerializableDtoData dto);
    }
}