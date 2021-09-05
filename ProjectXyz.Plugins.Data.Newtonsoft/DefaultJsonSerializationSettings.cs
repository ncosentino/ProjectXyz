namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class DefaultJsonSerializationSettings : IJsonSerializationSettings
    {
        public string SerializableIdPropertySerializedName { get; } = "i";

        public string DataPropertySerializedName { get; } = "d";
    }
}
