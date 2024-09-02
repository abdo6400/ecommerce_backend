using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ExtraInformation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public int ProductId { get; set; }

        // Navigation properties
        public  Product Product { get; set; } = null!;
    }
}