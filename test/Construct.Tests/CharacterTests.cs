namespace Construct.Tests;

public class CharacterTests
{
    [Theory]
    [InlineData((char)ushort.MaxValue, true)]
    [InlineData((char)ushort.MaxValue, false)]
    [InlineData((char)ushort.MinValue, true)]
    [InlineData((char)ushort.MinValue, false)]
    public void Should_serialize_char_to_bytes(char @char, bool toLittleEndian)
    {
        byte[] bytes;

        if (toLittleEndian)
        {
            bytes = BasicTypes.CharacterLittleEndian.Serialize(@char);
        }
        else
        {
            bytes = BasicTypes.CharacterBigEndian.Serialize(@char);
        }

        if (BitConverter.IsLittleEndian != toLittleEndian)
        {
            Array.Reverse(bytes);
        }

        Assert.Equal(2, bytes.Length);
        Assert.Equal(@char, BitConverter.ToUInt16(bytes));
    }

    [Theory]
    [InlineData(new byte[] { 0xff, 0x00 }, true)]
    [InlineData(new byte[] { 0xff, 0x00 }, false)]
    [InlineData(new byte[] { 0x00, 0xff }, true)]
    [InlineData(new byte[] { 0x00, 0xff }, false)]
    public void Should_deserialize_char_to_bytes(byte[] bytes, bool isLittleEndian)
    {
        char @char;

        if (isLittleEndian)
        {
            @char = BasicTypes.CharacterLittleEndian.Deserialize(bytes);
        }
        else
        {
            @char = BasicTypes.CharacterBigEndian.Deserialize(bytes);
        }

        Assert.Equal(BitConverter.ToUInt16(bytes), @char);
    }
}
