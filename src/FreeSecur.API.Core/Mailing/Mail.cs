using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.Mailing
{
    public class Mail
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string FromAddress { get; set; }
    }
}
