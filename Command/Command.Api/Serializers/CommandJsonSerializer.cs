using System;
using Core;
using Flurl.Http.Configuration;
using Newtonsoft.Json;

namespace Command.Api.Serializers;

public class CommandJsonSerializer : ISerializer
{
    private readonly JsonSerializerSettings settings = new()
    {
        Converters = [new EventModelConverter()],
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    };

    public T Deserialize<T>(string s)
    {
        return JsonConvert.DeserializeObject<T>(s, settings);
    }

    public T Deserialize<T>(Stream stream)
    {
        using var streamReader = new StreamReader(stream);
        return JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd(), settings);
    }

    public string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj, settings);
    }
}
