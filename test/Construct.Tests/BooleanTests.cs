namespace Construct.Tests;

public class BooleanTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Should_serialize_boolean_to_bytes(bool boolean)
    {
        byte[] bytes = BasicTypes.Boolean.Serialize(boolean);

        Assert.Single(bytes);
        Assert.Contains(BitConverter.GetBytes(boolean)[0], bytes);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    public void Should_deserialize_bytes_to_boolean(byte @byte)
    {
        bool value = BasicTypes.Boolean.Deserialize(new byte[] { @byte });

        Assert.Equal(BitConverter.ToBoolean(new byte[] { @byte }), value);
    
    }
}
