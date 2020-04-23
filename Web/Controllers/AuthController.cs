using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using ToolBox.Cryptography;
using Web.Infrastructure;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthRepository _authRepository;
        private readonly ICryptoRSA _cryptoRSA;

        public AuthController(AuthRepository authRepository, ICryptoRSA cryptoRSA)
        {
            _authRepository = authRepository;
            _cryptoRSA = cryptoRSA;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Register");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [GetPublicKey]
        public IActionResult Register(RegisterForm registerForm)
        {
            registerForm.Passwd = Convert.ToBase64String(_cryptoRSA.Crypter(registerForm.Passwd));

            try
            {
                if(ModelState.IsValid)
                {
                    _authRepository.Register(registerForm);
                    return RedirectToAction("Index", "Home");
                }

                return View(registerForm);
            }
            catch (Exception ex)
            {
                return View("Error");
            }            
        }
    }
}