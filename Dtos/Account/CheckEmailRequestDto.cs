using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class CheckEmailRequestDto
    {
        public string Email { get; set; } = null!;
    }
}