namespace Construct.Tests;

public class DoubleTests
{
    [Theory]
    [InlineData(double.MaxValue, true)]
    [InlineData(double.MaxValue, false)]
    [InlineData(double.MinValue, true)]
    [InlineData(double.MinValue, false)]
    public void Should_serialize_double_to_bytes(double number, bool toLittleEndian)
    {
        byte[] bytes;

        if (toLittleEndian)
        {
            bytes = BasicTypes.DoubleLittleEndian.Serialize(number);
        }
        else
        {
            bytes = BasicTypes.DoubleBigEndian.Serialize(number);
        }

        if (BitConverter.IsLittleEndian != toLittleEndian)
        {
            Array.Reverse(bytes);
        }

        Assert.Equal(8, bytes.Length);
        Assert.Equal(number, BitConverter.ToDouble(bytes));
    }

    [Theory]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, true)]
    [InlineData(new byte[] { 0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, false)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff }, true)]
    [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff }, false)]
    public void Should_deserialize_bytes_to_double(byte[] bytes, bool isLittleEndian)
    {
        double @double;

        if (isLittleEndian)
        {
            @double = BasicTypes.DoubleLittleEndian.Deserialize(bytes);
        }
        else
        {
            @double = BasicTypes.DoubleBigEndian.Deserialize(bytes);
        }

        Assert.Equal(BitConverter.ToDouble(bytes), @double);
    }
}
