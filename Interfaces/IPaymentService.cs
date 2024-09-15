using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IPaymentService
    {
        public  Task<string> CreatePaymentSession(Dictionary<string, string> data);
    }
}