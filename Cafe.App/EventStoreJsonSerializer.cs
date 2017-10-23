using System;
using EventFlow.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Cafe.App
{
    public class EventStoreJsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            Converters = { new StringEnumConverter() },
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };

        public string Serialize(object obj, bool indented = false)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, settings);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}