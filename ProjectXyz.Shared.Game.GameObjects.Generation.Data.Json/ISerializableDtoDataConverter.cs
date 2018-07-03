using Newtonsoft.Json.Linq;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface ISerializableDtoDataConverter
    {
        ISerializableDtoData Convert(JObject jsonObject);
    }
}