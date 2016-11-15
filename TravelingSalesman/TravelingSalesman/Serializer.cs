using Newtonsoft.Json;

namespace TravelingSalesman
{
    internal class Serializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public T Deserialize<T>(string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }
    }
}