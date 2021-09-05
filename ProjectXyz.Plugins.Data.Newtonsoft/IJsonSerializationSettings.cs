namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public interface IJsonSerializationSettings
    {
        string SerializableIdPropertySerializedName { get; }

        string DataPropertySerializedName { get; }
    }
}
