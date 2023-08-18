using System.Numerics;

namespace Construct;

public class Enumeration<TEnum, TInteger> : Construct<TEnum> where TEnum : struct, Enum where TInteger : IBinaryInteger<TInteger>
{
    private readonly IConstruct<TInteger> _integerConstruct;

    public Enumeration(IConstruct<TInteger> integerConstruct)
    {
        if (typeof(TEnum).GetEnumUnderlyingType() != typeof(TInteger))
        {
            throw new ArgumentException("The numeric type of the enumeration must match the dash you passed in");
        }

        _integerConstruct = integerConstruct;
    }

    public override TEnum Deserialize(byte[] bytes)
    {
        var integer = Convert.ChangeType(_integerConstruct.Deserialize(bytes), typeof(TInteger));

        return (TEnum)Enum.ToObject(typeof(TEnum), integer);
    }

    public override byte[] Serialize(TEnum @object)
    {
        TInteger integer = (TInteger)Convert.ChangeType(@object, @object.GetTypeCode());
        return _integerConstruct.Serialize(integer);
    }
}
