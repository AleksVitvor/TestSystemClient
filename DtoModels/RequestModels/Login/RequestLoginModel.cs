using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModels.RequestModels.Login
{
    public class RequestLoginModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}
