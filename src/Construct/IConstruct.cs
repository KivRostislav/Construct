namespace Construct;

public interface IConstruct<T> : ISerializer<T>, IDeserializer<T> where T : notnull { }

public abstract class Construct<T> : IConstruct<T> where T : notnull
{
    public abstract T Deserialize(byte[] bytes);

    public abstract byte[] Serialize(T @object);

    object IDeserializer.Deserialize(byte[] bytes)
    {
        return Deserialize(bytes);
    }
}

public interface ISerializer<T> where T : notnull
{
    byte[] Serialize(T @object);
}

public interface IDeserializer<T> : IDeserializer
{
    new T Deserialize(byte[] bytes);
}


public interface IDeserializer
{
    object Deserialize(byte[] bytes);
}