using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using ToolBox.Cryptography;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICrypto _cryptoService;

        public AuthController(ICrypto cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [Route("api/auth/register")]
        [HttpPost]
        public void Register(RegisterForm registerForm)
        {
            string password = _cryptoService.Decrypter(Convert.FromBase64String(registerForm.Password));

            //insert into database
        }
    }
}