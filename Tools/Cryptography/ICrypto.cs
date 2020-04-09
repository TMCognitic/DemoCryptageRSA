namespace ToolBox.Cryptography
{
    public interface ICrypto
    {
        byte[] BinaryBothKeys { get; }
        byte[] BinaryPublicKey { get; }
        string XmlBothKeys { get; }
        string XmlPublicKey { get; }

        byte[] Crypter(string ACrypter);
        string Decrypter(byte[] ADecrypter);
    }
}