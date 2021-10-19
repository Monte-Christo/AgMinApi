using System.Text.Json.Serialization;

[JsonSerializable(typeof(Person), GenerationMode = JsonSourceGenerationMode.Serialization)]
public partial class SampleJsonSerializerContext : JsonSerializerContext
{
}