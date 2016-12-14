namespace Pi3BackgroundApp.Common
{
    internal interface IDataBitConverter
    {
        float ToFloat(byte b);

        float ToFloat(byte[] bytes);

        int ToInt(byte b);

        int ToInt(byte[] bytes);

        byte[] FixEndianness(byte[] input, bool isBigEndHiBitFirst);

        byte[] SubArray(byte[] input, int start, int length);
    }
}