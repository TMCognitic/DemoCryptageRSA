using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Cryptography;

namespace Web.Infrastructure
{
    public interface ICryptoController
    {
        ICrypto CryptoService { get; set; }
    }
}
