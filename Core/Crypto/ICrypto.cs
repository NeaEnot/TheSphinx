namespace TheSphinx.Core.Crypto
{
    internal interface ICrypto
    {
        byte[] Encrypt(byte[] bytes, string password);
        byte[] Decrypt(byte[] bytes, string password);
    }
}
