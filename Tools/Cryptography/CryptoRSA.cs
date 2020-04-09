using System;
using System.Security.Cryptography;
using System.Text;

namespace ToolBox.Cryptography
{
    public class CryptoRSA : ICrypto
    {
        private RSACryptoServiceProvider _RSACSP;
        private RSAParameters _RSAParameters;
        private UTF8Encoding _Converter;
        private bool _OAEPPadding;

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

        public CryptoRSA(bool OAEPPadding = true, KeySizes KeySize = KeySizes.RSA2048)
        {
            _RSACSP = new RSACryptoServiceProvider((int)KeySize);
            _RSAParameters = _RSACSP.ExportParameters(!_RSACSP.PublicOnly);
            _OAEPPadding = OAEPPadding;
            _Converter = new UTF8Encoding();
        }

        public CryptoRSA(string XmlKeys, bool OAEPPadding = true, KeySizes KeySize = KeySizes.RSA2048) : this(OAEPPadding, KeySize)
        {
            if (XmlKeys != null)
            {
                _RSACSP.FromXmlString(XmlKeys);
            }

            _RSAParameters = _RSACSP.ExportParameters(!_RSACSP.PublicOnly);
        }

        public CryptoRSA(byte[] BinaryKeys, bool OAEPPadding = true, KeySizes KeySize = KeySizes.RSA2048) : this(OAEPPadding, KeySize)
        {
            if (BinaryKeys != null)
            {
                _RSACSP.ImportCspBlob(BinaryKeys);
            }

            _RSAParameters = _RSACSP.ExportParameters(!_RSACSP.PublicOnly);
        }

        public byte[] Crypter(string ACrypter)
        {
            if (ACrypter.Length > ((_RSACSP.KeySize / 8) - 42) / 2)
                throw new InvalidOperationException(string.Format("Chaine trop longue!!! (Taille Maximale : {0})", ((_RSACSP.KeySize / 8) - 42) / 2));

            byte[] ACrypterEnByte = _Converter.GetBytes(ACrypter);
            return _RSACSP.Encrypt(ACrypterEnByte, _OAEPPadding);
        }

        public string Decrypter(byte[] ADecrypter)
        {
            if (_RSACSP.PublicOnly)
            {
                throw new InvalidOperationException("Pour décrypter il vous faut la clé privée!!");
            }

            byte[] Decrypte = _RSACSP.Decrypt(ADecrypter, _OAEPPadding);
            return _Converter.GetString(Decrypte);
        }
    }
}
