using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ExtraInformation;
using api.Models;

namespace api.Mappers
{
    public static class ExtraInformationMappers
    {

        public static ExtraInformationDto ToExtraInformationDto(this ExtraInformation extraInformation)
        {

            return new ExtraInformationDto
            {
                Id = extraInformation.Id,
                Value = extraInformation.Value,
                Name = extraInformation.Name
            };
        }

    }
}