using System.Text;

namespace Construct.Types;

public class String : Construct<string>
{
    private readonly Encoding _encoding;

    public String(Encoding encoding)
    {
        _encoding = encoding;
    }

    public override string Deserialize(byte[] bytes)
    {
        return _encoding.GetString(bytes);
    }

    public override byte[] Serialize(string @object)
    {
        return _encoding.GetBytes(@object);
    }
}
