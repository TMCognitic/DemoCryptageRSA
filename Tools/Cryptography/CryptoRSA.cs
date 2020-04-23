using System;
using System.Security.Cryptography;
using System.Text;

namespace ToolBox.Cryptography
{
    public class CryptoRSA : ICryptoRSA
    {
        private RSACryptoServiceProvider _RSACSP;
        private UTF8Encoding _Converter;

        public string XmlPublicKey
        {
            get { return _RSACSP.ToXmlString(false); }
        }

        public string XmlBothKeys
        {
            get { return _RSACSP.ToXmlString(true); }
        }

        public byte[] BinaryPublicKey
        {
            get { return _RSACSP.ExportCspBlob(false); }
        }

        public byte[] BinaryBothKeys
        {
            get { return _RSACSP.ExportCspBlob(true); }
        }

        public CryptoRSA() : this(KeySizes.RSA2048)
        {

        }

        public CryptoRSA(KeySizes KeySize)
        {
            _RSACSP = new RSACryptoServiceProvider((int)KeySize);
            _Converter = new UTF8Encoding();
        }

        public CryptoRSA(string XmlKeys)
        {
            _RSACSP = new RSACryptoServiceProvider();
            _Converter = new UTF8Encoding();

            if (XmlKeys != null)
            {
                _RSACSP.FromXmlString(XmlKeys);
            }
        }

        public CryptoRSA(byte[] BinaryKeys)
        {
            _RSACSP = new RSACryptoServiceProvider();
            _Converter = new UTF8Encoding();

            if (BinaryKeys != null)
            {
                _RSACSP.ImportCspBlob(BinaryKeys);
            }
        }

        public byte[] Crypter(string ACrypter)
        {
            if (ACrypter.Length > ((_RSACSP.KeySize / 8) - 42) / 2)
                throw new InvalidOperationException(string.Format("Chaine trop longue!!! (Taille Maximale : {0})", ((_RSACSP.KeySize / 8) - 42) / 2));

            byte[] ACrypterEnByte = _Converter.GetBytes(ACrypter);
            return _RSACSP.Encrypt(ACrypterEnByte, true);
        }

        public string Decrypter(byte[] ADecrypter)
        {
            if (_RSACSP.PublicOnly)
            {
                throw new InvalidOperationException("Pour décrypter il vous faut la clé privée!!");
            }

            byte[] Decrypte = _RSACSP.Decrypt(ADecrypter, true);
            return _Converter.GetString(Decrypte);
        }

        public void ImportBinaryKeys(byte[] keys)
        {
            _RSACSP.ImportCspBlob(keys);
        }

        public void ImportXmlKeys(string xml)
        {
            _RSACSP.FromXmlString(xml);
        }
    }
}
