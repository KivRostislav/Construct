namespace Construct;

public interface IField
{
    void Deserialize(DeserializationContext context);

    void Serialize(SerializationContext context);
}

public class Field<T> : IField where T : notnull
{
    private readonly string _name;

    private readonly IConstruct<T> _construct;

    private readonly Func<DeserializationContext, int> _resolveSize;

    internal Field(string name, IConstruct<T> construct, Func<DeserializationContext, int> resolveSize)
    {
        _name = name;
        _construct = construct;
        _resolveSize = resolveSize;
    }

    public static Field<T> Create(string name, IConstruct<T> construct, int size)
    {
        return Create(name, construct, (_) => size);
    }

    public static Field<T> Create(string name, IConstruct<T> construct, string fieldName)
    {
        return Create(name, construct, (context) => Convert.ToInt32(context.Structures[fieldName]));
    }

    public static Field<T> Create(string name, IConstruct<T> construct, Func<DeserializationContext, int> resolveSize)
    {
        return new Field<T>(name, construct, resolveSize);
    }

    public void Deserialize(DeserializationContext context)
    {
        int fieldSize = _resolveSize(context);

        byte[] fieldPart = new byte[fieldSize];

        context.Buffer.Read(fieldPart, 0, fieldSize);

        T field = _construct.Deserialize(fieldPart);

        context.Structures.Add(_name, field);
    }

    public void Serialize(SerializationContext context)
    {
        if (!context.Structures.ContainsKey(_name))
        {
            throw new InvalidOperationException($"Could not determine the appropriate field for serialization that would correspond to \"{_name}\"");
        }

        byte[] bytes = _construct.Serialize((T)context.Structures[_name]);

        context.Buffer.Write(bytes);
    }
}
