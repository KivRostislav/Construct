using Construct.Pipeline;

namespace Construct;

public class Structure : Construct<Dictionary<string, object>>
{
    private readonly Action<SerializationContext> _serializer;

    private readonly Action<DeserializationContext> _deserializer;

    public Structure(IField[] fields)
    {
        var serializers = fields.Select<IField, Action<SerializationContext>>(x => x.Serialize).ToArray();
        var deserializers = fields.Select<IField, Action<DeserializationContext>>(x => x.Deserialize).ToArray();

        PipelineBuilder<SerializationContext> serializerPipeline = new();
        PipelineBuilder<DeserializationContext> deserializerPipeline = new();

        serializerPipeline.Register(serializers);
        deserializerPipeline.Register(deserializers);

        _serializer = serializerPipeline.Build();
        _deserializer = deserializerPipeline.Build();
    }

    public override Dictionary<string, object> Deserialize(byte[] bytes)
    {
        DeserializationContext context = new(bytes);

        _deserializer.Invoke(context);

        return context.Structures;
    }

    public override byte[] Serialize(Dictionary<string, object> @object)
    {
        SerializationContext context = new(@object);

        _serializer.Invoke(context);

        return context.Buffer.ToArray();
    }
}
