using System.Buffers.Binary;
using System.Collections.Immutable;

namespace Construct.Types;

public class Number<T> : Construct<T> where T : notnull
{
    private readonly int _numberSize;

    private readonly bool _isLittleEndian;

    public Number(int numberSize, bool isLittleEndian)
    {
        _numberSize = numberSize;
        _isLittleEndian = isLittleEndian;
    }

    public override T Deserialize(byte[] bytes)
    {
        ThrowHelper.ThrowIfNumberOfBytesIsNotEnough(bytes.Length, _numberSize);

        if (!NumberOperations.Deserializers.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException("No deserializer found for this type");
        }

        if (BitConverter.IsLittleEndian != _isLittleEndian)
        {
            Array.Reverse(bytes);
        }

        return (T)NumberOperations.Deserializers[typeof(T)](bytes);
    }

    public override byte[] Serialize(T number)
    {
        if (!NumberOperations.Serializers.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException("No serializer found for this type");
        }

        byte[] buffer = new byte[_numberSize];

        NumberOperations.Serializers[typeof(T)](buffer, number);

        if (!_isLittleEndian)
        {
            Array.Reverse(buffer);
        }

        return buffer;
    }
}

internal static class NumberOperations
{
    public static readonly ImmutableDictionary<Type, Func<byte[], object>> Deserializers = ImmutableDictionary.CreateRange(
        new KeyValuePair<Type, Func<byte[], object>>[]
        {
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(byte), (bytes) => bytes[0]),
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(sbyte), (bytes) => unchecked((sbyte)bytes[0])),
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(short), (bytes) => BinaryPrimitives.ReadInt16LittleEndian(bytes)),
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(ushort), (bytes) => BinaryPrimitives.ReadUInt16LittleEndian(bytes)),
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(int), (bytes) => BinaryPrimitives.ReadInt32LittleEndian(bytes)),
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(uint), (bytes) => BinaryPrimitives.ReadUInt32LittleEndian(bytes)),
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(long), (bytes) => BinaryPrimitives.ReadInt64LittleEndian(bytes)),
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(ulong), (bytes) => BinaryPrimitives.ReadUInt64LittleEndian(bytes)),
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(float), (bytes) => BinaryPrimitives.ReadSingleLittleEndian(bytes)),
            KeyValuePair.Create<Type, Func<byte[], object>>(typeof(double), (bytes) => BinaryPrimitives.ReadDoubleLittleEndian(bytes)),
        });

    public static readonly ImmutableDictionary<Type, Action<byte[], object>> Serializers = ImmutableDictionary.CreateRange(
        new KeyValuePair<Type, Action<byte[], object>>[]
        {
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(byte), (buffer, number) => buffer[0] = (byte)number),
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(sbyte), (buffer, number) => buffer[0] = unchecked((byte)(sbyte)number)),
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(short), (buffer, number) => BinaryPrimitives.WriteInt16LittleEndian(buffer, (short)number)),
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(ushort), (buffer, number) => BinaryPrimitives.WriteUInt16LittleEndian(buffer, (ushort)number)),
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(int), (buffer, number) => BinaryPrimitives.WriteInt32LittleEndian(buffer, (int)number)),
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(uint), (buffer, number) => BinaryPrimitives.WriteUInt32LittleEndian(buffer, (uint)number)),
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(long), (buffer, number) => BinaryPrimitives.WriteInt64LittleEndian(buffer, (long)number)),
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(ulong), (buffer, number) => BinaryPrimitives.WriteUInt64LittleEndian(buffer, (ulong)number)),
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(float), (buffer, number) => BinaryPrimitives.WriteSingleLittleEndian(buffer, (float)number)),
            KeyValuePair.Create<Type, Action<byte[], object>>(typeof(double), (buffer, number) => BinaryPrimitives.WriteDoubleLittleEndian(buffer, (double)number)),
        });
}
