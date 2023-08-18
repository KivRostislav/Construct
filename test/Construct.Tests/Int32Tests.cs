namespace Construct.Tests;

public class Int32Tests
{
    [Theory]
    [InlineData(int.MaxValue, true)]
    [InlineData(int.MaxValue, false)]
    [InlineData(int.MinValue, true)]
    [InlineData(int.MinValue, false)]
    public void Should_serialize_int32_to_bytes(int number, bool toLittleEndian)
    {
        byte[] bytes;

        if (toLittleEndian)
        {
            bytes = BasicTypes.Int32LittleEndian.Serialize(number);
        }
        else
        {
            bytes = BasicTypes.Int32BigEndian.Serialize(number);
        }

        if (BitConverter.IsLittleEndian != toLittleEndian)
        {
            Array.Reverse(bytes);
        }

        Assert.Equal(4, bytes.Length);
        Assert.Equal(number, BitConverter.ToInt32(bytes));
    }

    [Theory]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00 }, true)]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00 }, false)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0xff }, true)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0xff }, false)]
    public void Should_deserialize_bytes_to_int32(byte[] bytes, bool isLittleEndian)
    {
        int @int;

        if (isLittleEndian)
        {
            @int = BasicTypes.Int32LittleEndian.Deserialize(bytes);
        }
        else
        {
            @int = BasicTypes.Int32BigEndian.Deserialize(bytes);
        }

        Assert.Equal(BitConverter.ToInt32(bytes), @int);
    }

    [Theory]
    [InlineData(uint.MaxValue, true)]
    [InlineData(uint.MaxValue, false)]
    [InlineData(uint.MinValue, true)]
    [InlineData(uint.MinValue, false)]
    public void Should_serialize_uint32_to_bytes(uint number, bool toLittleEndian)
    {
        byte[] bytes;

        if (toLittleEndian)
        {
            bytes = BasicTypes.UInt32LittleEndian.Serialize(number);
        }
        else
        {
            bytes = BasicTypes.UInt32BigEndian.Serialize(number);
        }

        if (BitConverter.IsLittleEndian != toLittleEndian)
        {
            Array.Reverse(bytes);
        }

        Assert.Equal(4, bytes.Length);
        Assert.Equal(number, BitConverter.ToUInt32(bytes));
    }

    [Theory]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00 }, true)]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00 }, false)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0xff }, true)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0xff }, false)]
    public void Should_deserialize_bytes_to_uint32(byte[] bytes, bool isLittleEndian)
    {
        uint @int;

        if (isLittleEndian)
        {
            @int = BasicTypes.UInt32LittleEndian.Deserialize(bytes);
        }
        else
        {
            @int = BasicTypes.UInt32BigEndian.Deserialize(bytes);
        }

        Assert.Equal(BitConverter.ToUInt32(bytes), @int);
    }
}
