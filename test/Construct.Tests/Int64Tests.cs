namespace Construct.Tests;

public class Int64Tests
{
    [Theory]
    [InlineData(long.MaxValue, true)]
    [InlineData(long.MaxValue, false)]
    [InlineData(long.MinValue, true)]
    [InlineData(long.MinValue, false)]
    public void Should_serialize_int64_to_bytes(long number, bool toLittleEndian)
    {
        byte[] bytes;

        if (toLittleEndian)
        {
            bytes = BasicTypes.Int64LittleEndian.Serialize(number);
        }
        else
        {
            bytes = BasicTypes.Int64BigEndian.Serialize(number);
        }

        if (BitConverter.IsLittleEndian != toLittleEndian)
        {
            Array.Reverse(bytes);
        }

        Assert.Equal(8, bytes.Length);
        Assert.Equal(number, BitConverter.ToInt64(bytes));
    }

    [Theory]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}, true)]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, false)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff }, true)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff }, false)]
    public void Should_deserialize_bytes_to_int64(byte[] bytes, bool isLittleEndian)
    {
        long @int;

        if (isLittleEndian)
        {
            @int = BasicTypes.Int64LittleEndian.Deserialize(bytes);
        }
        else
        {
            @int = BasicTypes.Int64BigEndian.Deserialize(bytes);
        }

        Assert.Equal(BitConverter.ToInt64(bytes), @int);
    }

    [Theory]
    [InlineData(ulong.MaxValue, true)]
    [InlineData(ulong.MaxValue, false)]
    [InlineData(ulong.MinValue, true)]
    [InlineData(ulong.MinValue, false)]
    public void Should_serialize_uint64_to_bytes(ulong number, bool toLittleEndian)
    {
        byte[] bytes;

        if (toLittleEndian)
        {
            bytes = BasicTypes.UInt64LittleEndian.Serialize(number);
        }
        else
        {
            bytes = BasicTypes.UInt64BigEndian.Serialize(number);
        }

        if (BitConverter.IsLittleEndian != toLittleEndian)
        {
            Array.Reverse(bytes);
        }

        Assert.Equal(8, bytes.Length);
        Assert.Equal(number, BitConverter.ToUInt64(bytes));
    }

    [Theory]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, true)]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, false)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff }, true)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff }, false)]
    public void Should_deserialize_bytes_to_uint32(byte[] bytes, bool isLittleEndian)
    {
        ulong @int;

        if (isLittleEndian)
        {
            @int = BasicTypes.UInt64LittleEndian.Deserialize(bytes);
        }
        else
        {
            @int = BasicTypes.UInt64BigEndian.Deserialize(bytes);
        }

        Assert.Equal(BitConverter.ToUInt64(bytes), @int);
    }
}
