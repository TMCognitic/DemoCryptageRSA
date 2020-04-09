using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Cryptography;

namespace Web.Infrastructure
{
    [GetPublicKeyActionFilter]
    public class CryptoController : Controller
    {
        protected internal ICrypto CryptoServices { protected get; set; }
    }
}
