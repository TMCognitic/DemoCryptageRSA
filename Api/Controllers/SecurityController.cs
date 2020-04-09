using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using ToolBox.Cryptography;

namespace Api.Controllers
{
    public class SecurityController : ApiController
    {
        private readonly ICrypto _cryptoService;

        public SecurityController()
        {
            _cryptoService = new CryptoRSA(Properties.Resources.Keys);
        }

        public KeyInfo Get()
        {
            KeyInfo keyInfo = new KeyInfo() { PublicKey = Convert.ToBase64String(_cryptoService.BinaryPublicKey) };
            return keyInfo;
        }
    }
}