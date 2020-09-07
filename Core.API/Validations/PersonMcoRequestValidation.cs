using Domain.Custom_Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.API.Validations
{
    public class PersonMcoRequestValidation : AbstractValidator<PersonMcoRequest>
    {
        public PersonMcoRequestValidation()
        {
            RuleFor(c => c.PersonId).NotEmpty().WithMessage("No puede estar vacio");
            RuleFor(c => c.McoId).NotEmpty().WithMessage("No puede estar vacio");
            RuleFor(c => c.PersonId).GreaterThan(0).WithMessage("Debe ser mayor que 0");
            RuleFor(c => c.McoId).GreaterThan(0).WithMessage("Debe ser mayor que 0");
        }
    }    
}