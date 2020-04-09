using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ToolBox.Cryptography;

namespace Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly ICrypto _cryptoService;

        public AuthController()
        {
            _cryptoService = new CryptoRSA(Properties.Resources.Keys);
        }

        [Route("api/auth/register")]
        [HttpPost]
        public void Register(RegisterForm registerForm)
        {            
            string password = _cryptoService.Decrypter(Convert.FromBase64String(registerForm.Passwd));

            //insert into database
        }
    }
}