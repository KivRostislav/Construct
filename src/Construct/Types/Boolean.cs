namespace Construct.Types;

public class Boolean : Construct<bool>
{
    public override bool Deserialize(byte[] bytes)
    {
        if (bytes.Length < sizeof(bool))
        {
            throw new ArgumentException("The number of bytes is not enough to implement the type");
        }

        return BitConverter.ToBoolean(bytes);
    }

    public override byte[] Serialize(bool @object)
    {
        return BitConverter.GetBytes(@object);
    }
}
