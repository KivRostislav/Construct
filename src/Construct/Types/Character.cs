namespace Construct.Types;

public class Character : Construct<char>
{
    private readonly IConstruct<short> _int16Construct;

    public Character(bool isLittleEndian)
    {
        _int16Construct = new Number<short>(sizeof(short), isLittleEndian);
    }

    public override char Deserialize(byte[] bytes)
    {
        return (char)_int16Construct.Deserialize(bytes);
    }

    public override byte[] Serialize(char @object)
    {
        return _int16Construct.Serialize((short)@object);
    }
}
