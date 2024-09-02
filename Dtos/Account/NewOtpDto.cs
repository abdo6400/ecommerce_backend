using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class NewOtpDto
    {

        public string OtpSecret { get; set; } = string.Empty;
    }
}