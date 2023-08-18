using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construct;

public static class TypeHelper
{
    private static readonly TypeCode[] _integerCodes = new TypeCode[]
    {
        TypeCode.Byte,
        TypeCode.SByte,
        TypeCode.UInt16,
        TypeCode.UInt32,
        TypeCode.UInt64,
        TypeCode.Int16,
        TypeCode.Int32,
        TypeCode.Int64,
        TypeCode.Decimal,
        TypeCode.Double,
        TypeCode.Single,
    };

    public static bool IsInteger(this Type type)
    {
        return _integerCodes.Contains(Type.GetTypeCode(type));
    }

}
