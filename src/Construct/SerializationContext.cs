namespace Construct;

public class SerializationContext
{
    public readonly MemoryStream Buffer;

    public readonly Dictionary<string, object> Structures;
    
    public SerializationContext(Dictionary<string, object> structures)
    {
        Buffer = new();
        Structures = structures;
    }
}
