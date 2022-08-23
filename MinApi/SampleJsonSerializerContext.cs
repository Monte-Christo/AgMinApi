using System.Text.Json.Serialization;

namespace MinApi;

[JsonSerializable(typeof(Person), GenerationMode = JsonSourceGenerationMode.Serialization)]
public partial class SampleJsonSerializerContext : JsonSerializerContext
{
}