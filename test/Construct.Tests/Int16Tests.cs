namespace Construct.Tests;

public class Int16Tests
{
    [Theory]
    [InlineData(short.MaxValue, true)]
    [InlineData(short.MaxValue, false)]
    [InlineData(short.MinValue, true)]
    [InlineData(short.MinValue, false)]
    public void Should_serialize_int16_to_bytes(short number, bool toLittleEndian)
    {
        byte[] bytes;

        if (toLittleEndian)
        {
            bytes = BasicTypes.Int16LittleEndian.Serialize(number);
        }
        else
        {
            bytes = BasicTypes.Int16BigEndian.Serialize(number);
        }

        if (BitConverter.IsLittleEndian != toLittleEndian)
        {
            Array.Reverse(bytes);
        }

        Assert.Equal(2, bytes.Length);
        Assert.Equal(number, BitConverter.ToInt16(bytes));
    }

    [Theory]
    [InlineData(new byte[] { 0xff, 0x00 }, true)]
    [InlineData(new byte[] { 0xff, 0x00 }, false)]
    [InlineData(new byte[] { 0x00, 0xff }, true)]
    [InlineData(new byte[] { 0x00, 0xff }, false)]
    public void Should_deserialize_bytes_to_int8(byte[] bytes, bool isLittleEndian)
    {
        short @short;

        if (isLittleEndian)
        {
            @short = BasicTypes.Int16LittleEndian.Deserialize(bytes);
        }
        else
        {
            @short = BasicTypes.Int16BigEndian.Deserialize(bytes);
        }

        Assert.Equal(BitConverter.ToInt16(bytes), @short);
    }

    [Theory]
    [InlineData(ushort.MaxValue, true)]
    [InlineData(ushort.MaxValue, false)]
    [InlineData(ushort.MinValue, true)]
    [InlineData(ushort.MinValue, false)]
    public void Should_serialize_uint16_to_bytes(ushort number, bool toLittleEndian)
    {
        byte[] bytes;

        if (toLittleEndian)
        {
            bytes = BasicTypes.UInt16LittleEndian.Serialize(number);
        }
        else
        {
            bytes = BasicTypes.UInt16BigEndian.Serialize(number);
        }

        if (BitConverter.IsLittleEndian != toLittleEndian)
        {
            Array.Reverse(bytes);
        }

        Assert.Equal(2, bytes.Length);
        Assert.Equal(number, BitConverter.ToUInt16(bytes));
    }

    [Theory]
    [InlineData(new byte[] { 0xff, 0x00 }, true)]
    [InlineData(new byte[] { 0xff, 0x00 }, false)]
    [InlineData(new byte[] { 0x00, 0xff }, true)]
    [InlineData(new byte[] { 0x00, 0xff }, false)]
    public void Should_deserialize_bytes_to_uint8(byte[] bytes, bool isLittleEndian)
    {
        ushort @short;

        if (isLittleEndian)
        {
            @short = BasicTypes.UInt16LittleEndian.Deserialize(bytes);
        }
        else
        {
            @short = BasicTypes.UInt16BigEndian.Deserialize(bytes);
        }

        Assert.Equal(BitConverter.ToUInt16(bytes), @short);
    }
}
