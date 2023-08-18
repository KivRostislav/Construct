using Construct.Types;
using System.Text;

namespace Construct;

public static class BasicTypes
{
    public static readonly Types.Boolean Boolean = new();


    public static readonly Character CharacterLittleEndian = new(true);

    public static readonly Character CharacterBigEndian = new(false);


    public static readonly Types.String Utf8String = new(Encoding.UTF8);

    public static readonly Types.String Utf32String = new(Encoding.UTF32);

    public static readonly Types.String UnicodeLittleEndianString = new(Encoding.Unicode);

    public static readonly Types.String UnicodeBigEndianString = new(Encoding.BigEndianUnicode);

    public static readonly Types.String AsciiString = new(Encoding.ASCII);


    public static readonly Number<float> SingleLittleEndian = new(sizeof(float), true);

    public static readonly Number<float> SingleBigEndian = new(sizeof(float), false);


    public static readonly Number<double> DoubleLittleEndian = new(sizeof(double), true);

    public static readonly Number<double> DoubleBigEndian = new(sizeof(double), false);


    public static readonly Number<byte> UInt8 = new(sizeof(byte), true);

    public static readonly Number<sbyte> Int8 = new(sizeof(sbyte), true);


    public static readonly Number<short> Int16LittleEndian = new(sizeof(short), true);

    public static readonly Number<ushort> UInt16LittleEndian = new(sizeof(ushort), true);

    public static readonly Number<short> Int16BigEndian = new(sizeof(short), false);

    public static readonly Number<ushort> UInt16BigEndian = new(sizeof(ushort), false);


    public static readonly Number<int> Int32LittleEndian = new(sizeof(int), true);

    public static readonly Number<uint> UInt32LittleEndian = new(sizeof(uint), true);

    public static readonly Number<int> Int32BigEndian = new(sizeof(int), false);

    public static readonly Number<uint> UInt32BigEndian = new(sizeof(uint), false);


    public static readonly Number<long> Int64LittleEndian = new(sizeof(long), true);

    public static readonly Number<ulong> UInt64LittleEndian = new(sizeof(ulong), true);

    public static readonly Number<long> Int64BigEndian = new(sizeof(long), false);

    public static readonly Number<ulong> UInt64BigEndian = new(sizeof(ulong), false);
}
