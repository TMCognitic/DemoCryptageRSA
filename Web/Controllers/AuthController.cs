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
    public class AuthController : CryptoController
    {
        private readonly AuthRepository _authRepository;

        public AuthController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
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
        public IActionResult Register(RegisterForm registerForm)
        {
            registerForm.Passwd = Convert.ToBase64String(CryptoServices.Crypter(registerForm.Passwd));

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