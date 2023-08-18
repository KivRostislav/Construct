namespace Construct.Tests;

public class SingleTests
{
    [Theory]
    [InlineData(float.MaxValue, true)]
    [InlineData(float.MaxValue, false)]
    [InlineData(float.MinValue, true)]
    [InlineData(float.MinValue, false)]
    public void Should_serialize_single_to_bytes(float number, bool toLittleEndian)
    {
        byte[] bytes;

        if (toLittleEndian)
        {
            bytes = BasicTypes.SingleLittleEndian.Serialize(number);
        }
        else
        {
            bytes = BasicTypes.SingleBigEndian.Serialize(number);
        }

        if (BitConverter.IsLittleEndian != toLittleEndian)
        {
            Array.Reverse(bytes);
        }

        Assert.Equal(4, bytes.Length);
        Assert.Equal(number, BitConverter.ToSingle(bytes));
    }

    [Theory]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00 }, true)]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00 }, false)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0xff }, true)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0xff }, false)]
    public void Should_deserialize_bytes_to_single(byte[] bytes, bool isLittleEndian)
    {
        float single;

        if (isLittleEndian)
        {
            single = BasicTypes.SingleLittleEndian.Deserialize(bytes);
        }
        else
        {
            single = BasicTypes.SingleBigEndian.Deserialize(bytes);
        }

        Assert.Equal(BitConverter.ToSingle(bytes), single);
    }
}
