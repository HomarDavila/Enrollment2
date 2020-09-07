using Domain.Custom_Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.API.Validations
{
    public class PcpWithFiltersRequestValidation : AbstractValidator<PcpWithFiltersRequest>
    {
        public PcpWithFiltersRequestValidation()
        {

        }
    }
}