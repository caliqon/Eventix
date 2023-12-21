using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Cosmos;
using System.Text.Json;

namespace Eventix.State.Providers.CosmosDb.Serializers;

public class CosmosDbJsonSerializer : CosmosSerializer
{
    public override T FromStream<T>(Stream stream)
    {
        using (stream)
        {
            if (typeof(Stream).IsAssignableFrom(typeof(T)))
            {
                return (T)(object)stream;
            }

            using (var reader = new StreamReader(stream))
            {
                var text = reader.ReadToEnd();

                stream.Position = 0;

                return JsonSerializer.Deserialize<T>(text, GetSerializer());
            }
        }
    }

    public override Stream ToStream<T>(T input)
    {
        if (input == null)
            return Stream.Null;

        var streamPayload = new MemoryStream();

        using (var jsonWriter =
               new Utf8JsonWriter(streamPayload, new JsonWriterOptions { Indented = false }))
        {
            JsonSerializer.Serialize(jsonWriter, input, GetSerializer());
        }


        // No need to create a new MemoryStream object, just set the position back to the start of the stream
        streamPayload.Position = 0;
        
        return streamPayload;
    }

    private JsonSerializerOptions GetSerializer()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = false,
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode,
            WriteIndented = false,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        return options;
    }
}