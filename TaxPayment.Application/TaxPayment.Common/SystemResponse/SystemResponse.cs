using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.SystemResponse
{
    public class SystemResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string Extras { get; set; }
        public string ExtrasSecond { get; set; }
        public string ExtrasThird { get; set; }
    }
}
