using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using ToolBox.Cryptography;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private ICryptoRSA _cryptoService;

        public SecurityController(ICryptoRSA cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public KeyInfo Get()
        {
            KeyInfo keyInfo = new KeyInfo() { PublicKey = Convert.ToBase64String(_cryptoService.BinaryPublicKey) };
            return keyInfo;
        }
    }
}