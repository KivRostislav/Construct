using Shouldly;

namespace Construct.Tests;

public class SequenceTests
{
    [Fact]
    public void Should_serialize_sequence_to_bytes()
    {
        int[] numbers = new int[] { 1, 2, 3 };

        Sequence<int> sequence = new(sizeof(int), BasicTypes.Int32LittleEndian);

        byte[] bytes = sequence.Serialize(numbers);

        var encodedNumberSequence = numbers.SelectMany(x => BitConverter.GetBytes(x));

        if (!BitConverter.IsLittleEndian)
        {
            encodedNumberSequence = encodedNumberSequence.Reverse();
        }

        Assert.Equal(numbers.Length * sizeof(int), bytes.Length);
        bytes.ShouldBe(encodedNumberSequence);
    }

    [Fact]
    public void Should_deserialize_bytes_to_sequence()
    {
        int[] numbers = new int[] { 1, 2, 3 };
        byte[] bytes = numbers.SelectMany(x => BitConverter.GetBytes(x)).ToArray();

        Sequence<int> sequence;

        if (BitConverter.IsLittleEndian)
        {
            sequence = new(sizeof(int), BasicTypes.Int32LittleEndian);
        }
        else
        {
            sequence = new(sizeof(int), BasicTypes.Int32BigEndian);
        }

        int[] result = sequence.Deserialize(bytes).ToArray();

        Assert.Equal(numbers.Length, result.Length);
        result.ShouldBe(numbers);
    }
}
