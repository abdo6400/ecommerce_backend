using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ExtraInformation
{
    public class ExtraInformationDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string ValueEn { get; set; } = string.Empty;

        public string NameAr { get; set; } = string.Empty;
        public string ValueAr { get; set; } = string.Empty;

    }
}