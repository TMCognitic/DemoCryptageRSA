using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToolBox.Cryptography;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ICrypto _cryptoService;

        public SecurityController(ICrypto cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public string Get()
        {
            return Convert.ToBase64String(_cryptoService.BinaryPublicKey);

        }
    }
}