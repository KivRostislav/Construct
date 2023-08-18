namespace Construct.Tests;

public class Int8Tests
{
    [Theory]
    [InlineData(sbyte.MaxValue)]
    [InlineData(sbyte.MinValue)]
    public void Should_serialize_int8_to_bytes(sbyte number)
    {
        byte[] bytes = BasicTypes.Int8.Serialize(number);

        Assert.Single(bytes);
        Assert.Equal(number, unchecked((sbyte)bytes[0]));
    }

    [Theory]
    [InlineData(byte.MaxValue)]
    [InlineData(byte.MinValue)]
    public void Should_serialize_uint8_to_bytes(byte number)
    {
        byte[] bytes = BasicTypes.UInt8.Serialize(number);

        Assert.Single(bytes);
        Assert.Equal(number, (bytes[0]));
    }

    [Theory]
    [InlineData(new byte[] { 0xff })]
    [InlineData(new byte[] { 0x00 })]
    public void Should_deserialize_bytes_to_int8(byte[] bytes)
    {
        sbyte @sbyte = BasicTypes.Int8.Deserialize(bytes);

        Assert.Equal(bytes[0], unchecked((byte)@sbyte));
    }

    [Theory]
    [InlineData(new byte[] { 0xff })]
    [InlineData(new byte[] { 0x00 })]
    public void Should_deserialize_uint8_to_bytes(byte[] bytes)
    {
        byte @byte = BasicTypes.UInt8.Deserialize(bytes);

        Assert.Equal(bytes[0], @byte);
    }
}
