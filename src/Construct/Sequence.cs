namespace Construct;

public class Sequence<T> : Construct<T[]> where T : notnull
{
    private readonly IConstruct<T> _elementConstructor;

    private readonly int _elementSize;

    public Sequence(int elementSize, IConstruct<T> elementConstructor)
    {
        if (elementSize < 1)
        {
            throw new ArgumentException("Element size cannot be less than one");
        }

        _elementConstructor = elementConstructor;
        _elementSize = elementSize;
    }

    public override T[] Deserialize(byte[] bytes)
    { 
        if (bytes.Length % _elementSize != 0)
        {
            throw new ArgumentException("One of the array elements is not long enough for deserialization");
        }

        List<T> result = new();

        for (int i = 0; i < bytes.Length / _elementSize; i++)
        {
            byte[] bytePart = bytes.Skip(i * _elementSize).Take(_elementSize).ToArray();
            result.Add(_elementConstructor.Deserialize(bytePart));
        }

        return result.ToArray();
    }

    public override byte[] Serialize(T[] @object)
    {
        List<byte> result = new();

        foreach (T element in @object)
        {
            result.AddRange(_elementConstructor.Serialize(element));
        }

        return result.ToArray();
    }
}
