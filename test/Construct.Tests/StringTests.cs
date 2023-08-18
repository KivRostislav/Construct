using Shouldly;
using System.Text;

namespace Construct.Tests;

public class StringTests
{
    private readonly static Dictionary<Encoding, Construct.Types.String> _encodings = new()
    {
        { Encoding.UTF8, BasicTypes.Utf8String },
        { Encoding.UTF32, BasicTypes.Utf32String },
        { Encoding.Unicode, BasicTypes.UnicodeLittleEndianString },
        { Encoding.ASCII, BasicTypes.AsciiString },
    };

    private readonly static string _row = "_Ping";

    [Fact]
    public void Should_serialize_utf8_string_to_bytes()
    {
        Should_serialize_string_to_bytes(Encoding.UTF8);
    }

    [Fact]
    public void Should_serialize_utf32_string_to_bytes()
    {
        Should_serialize_string_to_bytes(Encoding.UTF32);
    }

    [Fact]
    public void Should_serialize_unicode_string_to_bytes()
    {
        Should_serialize_string_to_bytes(Encoding.Unicode);
    }

    [Fact]
    public void Should_serialize_ascii_string_to_bytes()
    {
        Should_serialize_string_to_bytes(Encoding.ASCII);
    }

    [Fact]
    public void Should_deserialize_bytes_to_utf8_string()
    {
        Should_deserialize_bytes_to_string(Encoding.UTF8);
    }

    [Fact]
    public void Should_deserialize_bytes_to_utf32_string()
    {
        Should_deserialize_bytes_to_string(Encoding.UTF32);
    }

    [Fact]
    public void Should_deserialize_bytes_to_unicode_string()
    {
        Should_deserialize_bytes_to_string(Encoding.Unicode);
    }

    [Fact]
    public void Should_deserialize_bytes_to_ascii_string()
    {
        Should_deserialize_bytes_to_string(Encoding.ASCII);
    }

    private static void Should_serialize_string_to_bytes(Encoding encoding)
    {
        byte[] bytes = _encodings[encoding].Serialize(_row);

        bytes.ShouldBe(encoding.GetBytes(_row));
    }

    private static void Should_deserialize_bytes_to_string(Encoding encoding)
    {
        byte[] bytes = encoding.GetBytes(_row);

        string @string = _encodings[encoding].Deserialize(bytes);

        Assert.Equal(encoding.GetString(bytes), @string);
    }
}