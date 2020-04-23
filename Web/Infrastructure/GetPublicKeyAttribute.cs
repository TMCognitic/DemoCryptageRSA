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
    public class GetPublicKeyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SecurityRepository securityRepository = (SecurityRepository)context.HttpContext.RequestServices.GetService(typeof(SecurityRepository));
            ICryptoRSA cryptoRSA = (ICryptoRSA)context.HttpContext.RequestServices.GetService(typeof(ICryptoRSA));
            KeyInfo keyInfo = securityRepository.Get();
            cryptoRSA.ImportBinaryKeys(Convert.FromBase64String(keyInfo.PublicKey));            
        }
    }
}
