using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Cryptography;

namespace Web.Infrastructure
{
    public class GetPublicKeyActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!(context.Controller is CryptoController cryptoController))
                throw new InvalidOperationException("Your controller must inherit of 'CryptoController' Type");

            SecurityRepository securityRepository = (SecurityRepository)context.HttpContext.RequestServices.GetService(typeof(SecurityRepository));
            KeyInfo keyInfo = securityRepository.Get();

            cryptoController.CryptoServices = new CryptoRSA(Convert.FromBase64String(keyInfo.PublicKey));            
        }
    }
}
