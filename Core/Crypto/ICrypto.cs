namespace TheSphinx.Core.Crypto
{
    internal interface ICrypto
    {
        string Encrypt(string text, string password);
        string Decrypt(string text, string password);
    }
}
