using Domain.Custom_Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.API.Validations
{
    public class PersonPcpPmgRequestValidation : AbstractValidator<PersonPcpPmgRequest>
    {
        public PersonPcpPmgRequestValidation()
        {
            RuleFor(c => c.PersonId).NotEmpty().WithMessage("No puede estar vacio");
            RuleFor(c => c.PcpId).NotEmpty().WithMessage("No puede estar vacio");
            RuleFor(c => c.PmgId).NotEmpty().WithMessage("No puede estar vacio");
            RuleFor(c => c.PersonId).GreaterThan(0).WithMessage("Debe ser mayor que 0");
            RuleFor(c => c.PcpId).GreaterThan(0).WithMessage("Debe ser mayor que 0");
            RuleFor(c => c.PmgId).GreaterThan(0).WithMessage("Debe ser mayor que 0");
        }
    }
}