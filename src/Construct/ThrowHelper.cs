namespace Construct;

public static class ThrowHelper
{
    public static void ThrowIfNumberOfBytesIsNotEnough()
    {
        throw new ArgumentException("The number of bytes is not enough to implement the type");
    }

    public static void ThrowIfNumberOfBytesIsNotEnough(int bytesLength, int typeSize)
    {
        if (bytesLength < typeSize)
        {
            ThrowIfNumberOfBytesIsNotEnough();
        }
    }
}
