using Shouldly;

namespace Construct.Tests;

public class StructureTests
{
    private readonly Dictionary<string, Structure> _structure = new()
    {
        { "static size", new Structure(new IField[]
        {
            Field<short>.Create("int16", BasicTypes.Int16LittleEndian, 2),
            Field<int>.Create("int32", BasicTypes.Int32LittleEndian, 4),
            Field<long>.Create("int64", BasicTypes.Int64LittleEndian, 8),
        }) },
        { "dynamic size", new Structure(new IField[]
        {
            Field<short>.Create("int16", BasicTypes.Int16LittleEndian, 2),
            Field<string>.Create("string", BasicTypes.UnicodeLittleEndianString, "int16"),
            Field<int>.Create("int32", BasicTypes.Int32LittleEndian, 4),
        }) },
    };

    private readonly Dictionary<string, Dictionary<string, object>> _objects = new()
    {
        { "static size", new Dictionary<string, object>()
        {
            { "int16", short.MaxValue },
            { "int32", int.MaxValue },
            { "int64", long.MaxValue },
        } },
        { "dynamic size", new Dictionary<string, object>()
        {
            { "int16", (short)4 },
            { "string", new string(new char[] { 'f', 'F' }) },
            { "int32", int.MaxValue },
        } },
    };

    private readonly Dictionary<string, byte[]> _decodedObjects = new()
    {
        { "static size", new byte[] { 0xff, 0x7f, 0xff, 0xff, 0xff, 0x7f, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x7f } },
        { "dynamic size", new byte[] { 0x04, 0x00, 0x66, 0x00, 0x46, 0x00, 0xff, 0xff, 0xff, 0x7f } },
    };

    [Theory]
    [InlineData("static size")]
    [InlineData("dynamic size")]
    public void Should_serialize_objects_to_bytes(string nameOfObject)
    {
        byte[] bytes = _structure[nameOfObject].Serialize(_objects[nameOfObject]);

        bytes.ShouldBe(_decodedObjects[nameOfObject]);
    }

    [Theory]
    [InlineData("static size")]
    [InlineData("dynamic size")]
    public void Should_deserialize_bytes_to_objects(string nameOfObject)
    {
        Dictionary<string, object> @object = _structure[nameOfObject].Deserialize(_decodedObjects[nameOfObject]);

        @object.ShouldBe(_objects[nameOfObject]);
    }
}
