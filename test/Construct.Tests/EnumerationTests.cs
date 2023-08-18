using System.Numerics;
using Shouldly;

namespace Construct.Tests;

public class EnumerationTests
{
    [Fact]
    public void Should_serialize_byte_enumeration_to_bytes()
    {
        Should_serialize_enumeration_to_bytes(ByteFlags.Open, BasicTypes.UInt8, new Enumeration<ByteFlags, byte>(BasicTypes.UInt8));
    }

    [Fact]
    public void Should_serialize_sbyte_enumeration_to_bytes()
    {
        Should_serialize_enumeration_to_bytes(SByteFlags.Open, BasicTypes.Int8, new Enumeration<SByteFlags, sbyte>(BasicTypes.Int8));
    }

    [Fact]
    public void Should_serialize_short_enumeration_to_bytes()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.Int16LittleEndian : BasicTypes.Int16BigEndian;

        Should_serialize_enumeration_to_bytes(ShortFlags.Open, integerConstruct, new Enumeration<ShortFlags, short>(integerConstruct));
    }

    [Fact]
    public void Should_serialize_ushort_enumeration_to_bytes()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.UInt16LittleEndian : BasicTypes.UInt16BigEndian;

        Should_serialize_enumeration_to_bytes(UShortFlags.Open, integerConstruct, new Enumeration<UShortFlags, ushort>(integerConstruct));
    }

    [Fact]
    public void Should_serialize_int_enumeration_to_bytes()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.Int32LittleEndian : BasicTypes.Int32BigEndian;

        Should_serialize_enumeration_to_bytes(IntFlags.Open, integerConstruct, new Enumeration<IntFlags, int>(integerConstruct));
    }

    [Fact]
    public void Should_serialize_uint_enumeration_to_bytes()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.UInt32LittleEndian : BasicTypes.UInt32BigEndian;

        Should_serialize_enumeration_to_bytes(UIntFlags.Open, integerConstruct, new Enumeration<UIntFlags, uint>(integerConstruct));
    }

    [Fact]
    public void Should_serialize_long_enumeration_to_bytes()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.Int64LittleEndian : BasicTypes.Int64BigEndian;

        Should_serialize_enumeration_to_bytes(LongFlags.Open, integerConstruct, new Enumeration<LongFlags, long>(integerConstruct));
    }

    [Fact]
    public void Should_serialize_ulong_enumeration_to_bytes()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.UInt64LittleEndian : BasicTypes.UInt64BigEndian;

        Should_serialize_enumeration_to_bytes(ULongFlags.Open, integerConstruct, new Enumeration<ULongFlags, ulong>(integerConstruct));
    }

    [Fact]
    public void Should_deserialize_bytes_to_byte_enumeration()
    {
        Should_deserialize_bytes_to_enumeration(new byte[] { (byte)ByteFlags.Open }, BasicTypes.UInt8, new Enumeration<ByteFlags, byte>(BasicTypes.UInt8));
    }

    [Fact]
    public void Should_deserialize_bytes_to_sbyte_enumeration()
    {
        Should_deserialize_bytes_to_enumeration(new byte[] { (byte)ByteFlags.Open }, BasicTypes.UInt8, new Enumeration<ByteFlags, byte>(BasicTypes.UInt8));
    }

    [Fact]
    public void Should_deserialize_bytes_to_short_enumeration()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.Int16LittleEndian : BasicTypes.Int16BigEndian;

        Should_deserialize_bytes_to_enumeration(BitConverter.GetBytes((short)ShortFlags.Open), integerConstruct, new Enumeration<ShortFlags, short>(integerConstruct));
    }

    [Fact]
    public void Should_deserialize_bytes_to_ushort_enumeration()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.UInt16LittleEndian : BasicTypes.UInt16BigEndian;

        Should_deserialize_bytes_to_enumeration(BitConverter.GetBytes((ushort)UShortFlags.Open), integerConstruct, new Enumeration<UShortFlags, ushort>(integerConstruct));
    }

    [Fact]
    public void Should_deserialize_bytes_to_int_enumeration()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.Int32LittleEndian : BasicTypes.Int32BigEndian;

        Should_deserialize_bytes_to_enumeration(BitConverter.GetBytes((int)IntFlags.Open), integerConstruct, new Enumeration<IntFlags, int>(integerConstruct));
    }

    [Fact]
    public void Should_deserialize_bytes_to_uint_enumeration()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.UInt32LittleEndian : BasicTypes.UInt32BigEndian;

        Should_deserialize_bytes_to_enumeration(BitConverter.GetBytes((uint)UIntFlags.Open), integerConstruct, new Enumeration<UIntFlags, uint>(integerConstruct));
    }

    [Fact]
    public void Should_deserialize_bytes_to_long_enumeration()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.Int64LittleEndian : BasicTypes.Int64BigEndian;

        Should_deserialize_bytes_to_enumeration(BitConverter.GetBytes((long)LongFlags.Open), integerConstruct, new Enumeration<LongFlags, long>(integerConstruct));
    }

    [Fact]
    public void Should_deserialize_bytes_to_ulong_enumeration()
    {
        var integerConstruct = BitConverter.IsLittleEndian ? BasicTypes.UInt64LittleEndian : BasicTypes.UInt64BigEndian;

        Should_deserialize_bytes_to_enumeration(BitConverter.GetBytes((ulong)ULongFlags.Open), integerConstruct, new Enumeration<ULongFlags, ulong>(integerConstruct));
    }

    private static void Should_serialize_enumeration_to_bytes<TEnum, TInteger>(TEnum @enum, IConstruct<TInteger> integerConstruct, Enumeration<TEnum, TInteger> enumeration) where TEnum : struct, Enum where TInteger : IBinaryInteger<TInteger>
    {
        byte[] bytes = enumeration.Serialize(@enum);

        TInteger integer = (TInteger)Convert.ChangeType(@enum, @enum.GetTypeCode());
        bytes.ShouldBe(integerConstruct.Serialize(integer));
    }

    private static void Should_deserialize_bytes_to_enumeration<TEnum, TInteger>(byte[] bytes, IConstruct<TInteger> integerConstruct, Enumeration<TEnum, TInteger> enumeration) where TEnum : struct, Enum where TInteger : IBinaryInteger<TInteger>
    {
        TEnum @enum = enumeration.Deserialize(bytes);

        TInteger integer = (TInteger)Convert.ChangeType(@enum, @enum.GetTypeCode());
        integerConstruct.Deserialize(bytes).ShouldBe(integer);
    }
}

internal enum ByteFlags : byte
{
    Open = byte.MaxValue - 2,
    Close = byte.MaxValue - 1,
    None = byte.MaxValue,
}

internal enum SByteFlags : sbyte
{
    Open = sbyte.MinValue,
    Close = sbyte.MinValue + 1,
    None = sbyte.MinValue + 2,
}

internal enum ShortFlags : short
{
    Open = short.MinValue,
    Close = short.MinValue + 1,
    None = short.MinValue + 2,
}

internal enum UShortFlags : ushort
{
    Open = ushort.MaxValue - 2,
    Close = ushort.MaxValue -1,
    None = ushort.MaxValue,
}

internal enum IntFlags : int
{
    Open = int.MinValue,
    Close = int.MinValue + 1,
    None = int.MinValue + 2,
}

internal enum UIntFlags : uint
{
    Open = uint.MaxValue - 2,
    Close = uint.MaxValue - 1,
    None = uint.MaxValue,
}

internal enum LongFlags : long
{
    Open = long.MinValue,
    Close = long.MinValue + 1,
    None = long.MinValue + 2,
}

internal enum ULongFlags : ulong
{
    Open = ulong.MaxValue - 2,
    Close = ulong.MaxValue - 1,
    None = ulong.MaxValue,
}