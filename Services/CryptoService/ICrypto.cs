using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CryptoService
{
    public interface ICrypto
    {
        public string SHA256GetHash(string source);
    }
}
