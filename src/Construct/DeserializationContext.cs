namespace Construct;

public class DeserializationContext
{
    public readonly MemoryStream Buffer;

    public readonly Dictionary<string, object> Structures;

    public DeserializationContext(byte[] bytes)
    {
        Buffer = new(bytes);
        Structures = new();
    }
}
