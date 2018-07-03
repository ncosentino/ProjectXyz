using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public sealed class DtoJsonSerializer : IDtoSerializer
    {
        public Stream Serialize(ISerializableDto dto)
        {
            var serialized = JsonConvert.SerializeObject(dto);
            var outStream = new MemoryStream();
            using (var writer = new StreamWriter(outStream, Encoding.UTF8, 4096, true))
            {
                writer.Write(serialized);
            }

            outStream.Position = 0;
            return outStream;
        }
    }
}